using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string scene;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(scene);
    }
}
