using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCameraEvent : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem gamesave;

    public GameEvent _event;
    public CinemachineVirtualCamera characterCM;
    private void Start()
    {
        if(_event.hasaved)
        {
            gameObject.GetComponent<CinemachineBlendListCamera>().m_Instructions[0].m_VirtualCamera = characterCM;
        }
    }
    private void Update()
    {
        Invoke("Action", 1.5f);
    }
    public void Action()
    {
        _event.hasaved = true;
        gamesave.Save();
    }
}
