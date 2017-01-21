using System;
using System.Text;
using System.Collections.Generic;

namespace Shared.Extensions
{
	public static class TimeSpanExtensions
	{
		public static string ToSimpleString (this TimeSpan timeSpan)
		{
			var text = string.Format("{0:00}:{1:00}:{2:00}", (int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
			if (timeSpan.TotalDays >= 1)
			{
				text = string.Format("{0:00}:{1}", (int)timeSpan.TotalDays, text);
			}
			return text;
		}

		public static string ToMinuteAndSecondsString (this TimeSpan timeSpan, bool ceilSeconds = true)
		{
			if (ceilSeconds)
			{
				timeSpan = TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds));
			}
			return string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		}

		/// <summary>
		/// Formats the TimeSpam into a string. If the numbers of hours are less than 1 they will added to the formated string.
		/// </summary>
		/// <returns>The hour and minute and seconds formatted string. If the numbers of hours are less than 1 they will added to the formated string.</returns>
		/// <param name="timeSpan">Time span.</param>
		/// <param name="ceilSeconds">If set to <c>true</c> ceil seconds.</param>
		public static string ToHourAndMinuteAndSecondsString (this TimeSpan timeSpan, bool ceilSeconds = true)
		{
			if (ceilSeconds)
			{
				timeSpan = TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds));
			}

			if (timeSpan.TotalHours >= 1)
			{
				return timeSpan.ToString(3);
			}

			return timeSpan.ToString(2);
		}

		static readonly Dictionary<TimeSpanField, string[]> DEFAULT_SUFFIXES = new Dictionary<TimeSpanField, string[]> {
			{TimeSpanField.Day, new[] {"d ", "d "}},
			{TimeSpanField.Hour, new[] {":", ":"}},
			{TimeSpanField.Minute, new[] {":", ":"}},
			{TimeSpanField.Second, new[] {":", ":"}}
		};

		public static string ToString (this TimeSpan timeSpan, int compartments, Dictionary<TimeSpanField, string[]> fieldSuffixes = null,
										bool addLastSuffix = false, TimeSpanField minimumField = TimeSpanField.Second, TimeSpanField maxField = TimeSpanField.Day)
		{
			var builder = new StringBuilder();
			int includedCompartments = 0;

			var minOffset = (int)TimeSpanField.Millisecond - (int)minimumField;
			fieldSuffixes = fieldSuffixes ?? DEFAULT_SUFFIXES;
			string lastSuffix = null;
			if (maxField <= TimeSpanField.Day && ((int)timeSpan.TotalDays > 0 || includedCompartments < compartments - 4 + minOffset) &&
				includedCompartments < compartments)
			{
				lastSuffix = AddToString(builder, TimeSpanField.Day, maxField, timeSpan.TotalDays, timeSpan.Days, "{0:0}{1}", fieldSuffixes);
				includedCompartments++;
			}

			if (maxField <= TimeSpanField.Hour &&
				((int)timeSpan.TotalHours > 0 || includedCompartments > 0 || includedCompartments < compartments - 3 + minOffset) &&
				includedCompartments < compartments)
			{
				lastSuffix = AddToString(builder, TimeSpanField.Hour, maxField, timeSpan.TotalHours, timeSpan.Hours, "{0:00}{1}", fieldSuffixes);
				includedCompartments++;
			}

			if (maxField <= TimeSpanField.Minute &&
				((int)timeSpan.TotalMinutes > 0 || includedCompartments > 0 || includedCompartments < compartments - 2 + minOffset) &&
				includedCompartments < compartments)
			{
				lastSuffix = AddToString(builder, TimeSpanField.Minute, maxField, timeSpan.TotalMinutes, timeSpan.Minutes, "{0:00}{1}", fieldSuffixes);
				includedCompartments++;
			}

			if (maxField <= TimeSpanField.Second &&
				((int)timeSpan.TotalSeconds > 0 || includedCompartments > 0 || includedCompartments < compartments - 1 + minOffset) &&
				includedCompartments < compartments)
			{
				lastSuffix = AddToString(builder, TimeSpanField.Second, maxField, timeSpan.TotalSeconds, timeSpan.Seconds, "{0:00}{1}", fieldSuffixes);
				includedCompartments++;
			}

			if (maxField <= TimeSpanField.Millisecond && ((int)timeSpan.TotalMilliseconds > 0 || includedCompartments > minOffset) &&
				includedCompartments < compartments)
			{
				lastSuffix = AddToString(builder, TimeSpanField.Millisecond, maxField, timeSpan.TotalMilliseconds, timeSpan.Milliseconds, "{0:000}{1}",
					fieldSuffixes);
			}

			if (!addLastSuffix && lastSuffix != null)
			{
				builder.Remove(builder.Length - lastSuffix.Length, lastSuffix.Length);
			}
			return builder.ToString();
		}

		static string AddToString (StringBuilder builder, TimeSpanField field, TimeSpanField maxField, double amountTotal, double amountSingle,
									string format, Dictionary<TimeSpanField, string[]> suffixes)
		{
			var amount = (int)maxField - (int)field == 0 ? amountTotal : amountSingle;
			var suffix = GetSuffix(field, amount, suffixes);
			builder.Append(string.Format(format, amount, suffix));
			return suffix;
		}

		static string GetSuffix (TimeSpanField field, double amount, Dictionary<TimeSpanField, string[]> suffixes)
		{
			return DictionaryExtensions.GetValueOrDefault(suffixes, field, new[] {":", ":"})[amount >= 2 ? 1 : 0];
		}
	}

	public enum TimeSpanField
	{
		Day,
		Hour,
		Minute,
		Second,
		Millisecond
	}
}