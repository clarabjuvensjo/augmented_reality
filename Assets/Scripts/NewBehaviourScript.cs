using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;



public class VideoPlayer : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManeger;

    private void awake()
    {
        _arTrackedImageManeger = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _arTrackedImageManeger.trackedImagesChanged += OnImagesChanged;
    }

    public void OnDisable()
    
    {
         _arTrackedImageManeger.trackedImagesChanged -= OnImagesChanged;
    }

    public void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trachedImage in args.added)
        {
            Debug.Log(trachedImage.name);
        }
    }
}
