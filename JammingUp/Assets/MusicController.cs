using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] GameObject map;
    private MapController mapController;
    private AudioSource audioSource;

    private void Awake()
    {
        mapController = map.GetComponent<MapController>();
    }
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.pitch = Mathf.Max(3f - mapController.GetMoveTick(), 1f);
    }
}
