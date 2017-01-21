using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using System.Collections;
using System.Diagnostics;

namespace Shared.Extensions
{
	public static class TypeExtensions
	{
		public static string GetTypeName (this Type t)
		{
			if (!t.IsGenericType) return t.Name;
			if (t.IsNested && t.DeclaringType.IsGenericType) throw new NotImplementedException();
			string txt = t.Name.Substring(0, t.Name.IndexOf('`')) + "<";
			int cnt = 0;
			foreach (Type arg in t.GetGenericArguments())
			{
				if (cnt > 0) txt += ", ";
				txt += GetTypeName(arg);
				cnt++;
			}
			return txt + ">";
		}
	}

	public class NameOf<T>
	{
		public static string Property<TProp> (Expression<Func<T, TProp>> expression, bool addSpaceBeforeUppercaseLetter = true)
		{
			var body = expression.Body as MemberExpression;
			if (body == null)
			{
				throw new ArgumentException("'expression' should be a member expression");
			}

			if (addSpaceBeforeUppercaseLetter)
			{
				return Regex.Replace(body.Member.Name, "[A-Z]", " $0").Trim();
			}
			else
			{
				return body.Member.Name;
			}
		}
	}

	public static class PropertyUtil
	{
		public static TProp GetProperty<TObject, TProp> (this TObject type, Expression<Func<TObject, TProp>> propertyRefExpr)
		{
			if (type == null)
			{
				return default(TProp);
			}

			return propertyRefExpr.Compile()(type);
		}
	}


	public class MemberInfoOf<T>
	{
		public static MemberInfo Property<TProp> (Expression<Func<T, TProp>> expression)
		{
			var body = expression.Body as MemberExpression;
			if (body == null)
			{
				throw new ArgumentException("'expression' should be a member expression");
			}

			return body.Member;
		}
	}

	public static class ReflectionExtension
	{
		public static Type GetUnderlyingType (this MemberInfo member)
		{
			switch (member.MemberType)
			{
				case MemberTypes.Event:
					return ((EventInfo)member).EventHandlerType;
				case MemberTypes.Field:
					return ((FieldInfo)member).FieldType;
				case MemberTypes.Method:
					return ((MethodInfo)member).ReturnType;
				case MemberTypes.Property:
					return ((PropertyInfo)member).PropertyType;
				default:
					throw new ArgumentException("Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo");
			}
		}

		public static bool IsOfGenericType (this Type instanceType, Type genericType)
		{
			while (instanceType != null)
			{
				if (instanceType.IsGenericType &&
					instanceType.GetGenericTypeDefinition() == genericType)
				{
					return true;
				}
				instanceType = instanceType.BaseType;
			}
			return false;
		}

	}

	public class TypeOf<T>
	{
		public static Type Property<TProp> (object obj, Expression<Func<T, TProp>> expression)
		{
			var name = NameOf<T>.Property(expression, false);
			return obj.GetType().GetMember(name)[0].GetUnderlyingType();
		}
	}

	public static class ReflectionUtils
	{

		public static object GetValueInObjectViaReflection (object source, string name)
		{
			if (source == null)
			{
				return null;
			}

			var type = source.GetType();

			//first try field access
			var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (field != null)
			{
				return field.GetValue(source);
			}

			//then property access
			var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (property != null)
			{
				return property.GetValue(source, null);
			}

			return null;
		}

		public static object GetValueInEnumerableViaReflection (object source, string name, int index)
		{
			var enumerable = GetValueInObjectViaReflection(source, name) as IEnumerable;
			var enumerator = enumerable.GetEnumerator();
			while (index >= 0)
			{
				if (enumerator.MoveNext())
				{
					index--;
				}
				else
				{
					return null; // Activator.CreateInstance(enumerable.GetType().GetGenericArguments()[0]);
				}
			}
			return enumerator.Current;
		}

		public static string GetCurrentClassAndMethodName ()
		{
			StackFrame sf = new StackTrace().GetFrame(2);
			return sf.GetMethod().DeclaringType.Name + "." + sf.GetMethod().Name;
		}
	}
}