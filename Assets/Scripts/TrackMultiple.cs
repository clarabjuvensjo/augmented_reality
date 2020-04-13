using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackMultiple : MonoBehaviour
{
    [Header("The length of this list must match the number of images in Reference Image Library")]
    public List<GameObject> ObjectsToPlace;

    private int refImageCount;
    private Dictionary<string, GameObject> allObjects;
    private ARTrackedImageManager arTrackedImageManager;
    private IReferenceImageLibrary refLibrary;
    private Vector3 fp; // First touch position
    private Vector3 lp; // Last touch position
    private Vector3 swipe;
    private float dragDistance; // minimum distance for a swipe to be registered


    // Måste denna vara public?
    public GameObject activeGameObject;
    private ARTracked​Image activeImage;

    public void Update()
    {
        if (activeImage != null && activeImage.trackingState != TrackingState.Tracking && activeGameObject != null && activeGameObject.activeSelf)
        {
            activeGameObject.SetActive(false);
        }
        if (activeImage != null && activeImage.trackingState == TrackingState.Tracking && activeGameObject != null && !activeGameObject.activeSelf)
        {
            activeGameObject.SetActive(true);
        }
        if (Input.touchCount == 1) // user is touching the screen with a single touch 
        {
            Touch touch1 = Input.GetTouch(0); // Get the touch
            if (touch1.phase == TouchPhase.Began) // Check for the first touch
            {
                fp = touch1.position;
            }

            else if (touch1.phase == TouchPhase.Ended) // Check if the finger is removed from the screen
            {
                lp = touch1.position;
                swipe = lp - fp;
                if (swipe.x >= dragDistance) // Positive value, swiping right
                {
                    SceneManager.LoadScene(4);
                }
                if (swipe.x <= -dragDistance) // Negative value, swiping left
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
        return;
    }

    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void Start()
    {
        refLibrary = arTrackedImageManager.referenceLibrary;
        refImageCount = refLibrary.count;
        LoadObjectDictionary();
        dragDistance = Screen.height * 15 / 100; // Dragdistance is 15% height of the screen
    }

    void LoadObjectDictionary()
    {
        allObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < refImageCount; i++)
        {
            allObjects.Add(refLibrary[i].name, ObjectsToPlace[i]);
            ObjectsToPlace[i].SetActive(false);
        }
    }

    void ActivateTrackedObject(ARTracked​Image trackedImage)
    {
        activeGameObject = allObjects[trackedImage.referenceImage.name];
        activeGameObject.SetActive(true);
        activeImage = trackedImage;
    }

    void DeActivateTrackedObject(ARTracked​Image trackedImage)
    {
        activeGameObject.SetActive(false);
        activeGameObject = null;
        activeImage = null;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args)
    {
        foreach (var addedImage in _args.added)
        {
            if (activeImage != null)
            {
                DeActivateTrackedObject(activeImage);
            }
            ActivateTrackedObject(addedImage);
        }

        foreach (var updated in _args.updated)
        {
            allObjects[updated.referenceImage.name].transform.position = updated.transform.position;
            allObjects[updated.referenceImage.name].transform.rotation = updated.transform.rotation;
        }
    }
}
