using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    GameObject SpaceText;
    [SerializeField]
    GameObject HockeyText;
    [SerializeField]
    GameObject UsableInterface;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(2.5f);
        SpaceText.SetActive(false);
        HockeyText.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        HockeyText.SetActive(false);
        UsableInterface.SetActive(true);
    }
}
