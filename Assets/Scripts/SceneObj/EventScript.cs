using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventScript : MonoBehaviour
{
    private GameObject eventObj;
    
    // 오브젝트 활성화
    public void ViewEventObj()
    {
        eventObj.SetActive(true);
    }
    
    // 개별 이벤트 요소
    public virtual void EventStart()
    {
        Debug.LogError("이벤트 시작");
    }
}
