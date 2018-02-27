/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {
    // public AudioSource audioSource;
    // public Button button;
    // public Sprite s1, s2;
    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler (this);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged (
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound ();
        } else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
            newStatus == TrackableBehaviour.Status.NOT_FOUND) {
            Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost ();
        } else {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost ();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    protected virtual void OnTrackingFound () {
        var rendererComponents = GetComponentsInChildren<Renderer> (true);
        var colliderComponents = GetComponentsInChildren<Collider> (true);
        var canvasComponents = GetComponentsInChildren<Canvas> (true);
        var particleComponents = GetComponentsInChildren<ParticleSystem> (true);
        var lightComponents = GetComponentsInChildren<Light> (true);
        var audioComponent = GetComponent<audioSourceScript> ();

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
        foreach (var component in particleComponents)
            component.Play ();
        foreach (var component in lightComponents)
            component.enabled = true;

        audioComponent.enabled = true;

    }

    protected virtual void OnTrackingLost () {
        var rendererComponents = GetComponentsInChildren<Renderer> (true);
        var colliderComponents = GetComponentsInChildren<Collider> (true);
        var canvasComponents = GetComponentsInChildren<Canvas> (true);
        var particleComponents = GetComponentsInChildren<ParticleSystem> (true);
        var lightComponents = GetComponentsInChildren<Light> (true);
        var audioComponent = GetComponent<audioSourceScript> ();
        if (audioComponent == null)
            Debug.Break ();
        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
        foreach (var component in particleComponents)
            component.Stop ();
        foreach (var component in lightComponents)
            component.enabled = false;

        audioComponent.enabled = false;

    }

    #endregion // PRIVATE_METHODS
}