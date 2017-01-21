using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
  public PointerListenerGroup onClick = new PointerListenerGroup ();
  public PointerListenerGroup onPointerDown = new PointerListenerGroup ();
  public PointerListenerGroup onPointerUp = new PointerListenerGroup ();
  public PointerListenerGroup onBeginDrag = new PointerListenerGroup ();
  public PointerListenerGroup onEndDrag = new PointerListenerGroup ();
  public PointerListenerGroup onDrag = new PointerListenerGroup ();
  public void OnPointerDown (PointerEventData eventData)
  {
    onPointerDown.InvokeAll (eventData);
  }

  public void OnPointerUp (PointerEventData eventData)
  {
    onPointerUp.InvokeAll (eventData);
  }

  public void OnPointerClick (PointerEventData eventData)
  {
    onClick.InvokeAll (eventData);
  }

  public void OnBeginDrag (PointerEventData eventData)
  {
    onBeginDrag.InvokeAll (eventData);
  }

  public void OnEndDrag (PointerEventData eventData)
  {
    onEndDrag.InvokeAll (eventData);
  }

  public void OnDrag (PointerEventData eventData)
  {
    onDrag.InvokeAll (eventData);
  }

  public void RemoveAllListeners ()
  {
    onClick.RemoveAllListeners ();
    onPointerDown.RemoveAllListeners ();
    onPointerUp.RemoveAllListeners ();
  }
}

public class PointerListenerGroup
{
  private List<Action<PointerEventData>> _listeners = new List<Action<PointerEventData>> ();
  public void AddListener (Action<PointerEventData> listener)
  {
    _listeners.Add (listener);
  }

  public void RemoveListener (Action<PointerEventData> listener)
  {
    _listeners.Remove (listener);
  }

  public void RemoveAllListeners ()
  {
    _listeners.Clear ();
  }

  public void InvokeAll (PointerEventData e)
  {
    foreach (var listener in _listeners.ToList ())
    {
      if (listener != null)
      {
        listener (e);
      }
    }
  }
}