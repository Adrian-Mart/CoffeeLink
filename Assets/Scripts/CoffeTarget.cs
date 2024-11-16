using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeTarget : MonoBehaviour
{
    [SerializeField] private CoffeSO coffeSO;
    [SerializeField] private GameObject starsPrefab;
    [SerializeField] private GameObject starsParent;
    [SerializeField] private Transform[] starsPositions;
    [SerializeField] private Material starsEnabled;
    [SerializeField] private Material starsDisabled;

    [SerializeField] private GameObject beanParent;
    [SerializeField] private GameObject aromaParent;
    [SerializeField] private GameObject tasteParent;

    public CoffeSO CoffeSO { get => coffeSO; }

    public void UpdateCoffeInfo(ARState state)
    {
        switch (state)
        {
            case ARState.ShowTaste:
                starsParent.SetActive(false);
                beanParent.SetActive(false);
                aromaParent.SetActive(false);
                tasteParent.SetActive(true);
                break;
            case ARState.ShowAroma:
                starsParent.SetActive(false);
                beanParent.SetActive(false);
                aromaParent.SetActive(true);
                tasteParent.SetActive(false);
                break;
            case ARState.ShowRating:
                SetStars();
                starsParent.SetActive(true);
                beanParent.SetActive(false);
                aromaParent.SetActive(false);
                tasteParent.SetActive(false);
                break;
            case ARState.ShowMap:
                starsParent.SetActive(false);
                beanParent.SetActive(false);
                aromaParent.SetActive(false);
                tasteParent.SetActive(false);
                break;
            case ARState.ShowBean:
                starsParent.SetActive(false);
                beanParent.SetActive(true);
                aromaParent.SetActive(false);
                tasteParent.SetActive(false);
                break;
            default:
                starsParent.SetActive(false);
                beanParent.SetActive(false);
                aromaParent.SetActive(false);
                tasteParent.SetActive(false);
                break;
        }
    }

    void Start()
    {
        UpdateCoffeInfo(ARState.NotDetected);
        for (int i = 0; i < 5; i++)
        {
            GameObject star = Instantiate(starsPrefab, starsPositions[i]);
            star.GetComponentInChildren<MeshRenderer>().material = starsDisabled;
            star.SetActive(true);
        }
        SetStars();
    }

    private void SetStars()
    {   
        int stars = PlayerPrefs.GetInt(coffeSO.CoffeName, -1);

        for (int i = 0; i < 5; i++)
        {
            starsParent.transform.GetChild(i).GetChild(0).GetComponentInChildren<MeshRenderer>().material = i < stars ? starsEnabled : starsDisabled;
        }
    }
}
