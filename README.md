# UnityVRStarterKit

The Unity VR Starter kit is a empty Unity project already prepared with a number of components to start developing with Unity for VR using the HTC Vive and optionally Optitrack MOCAP system. With this Unity project you can:
Import you Titlbrush drawings
Import you Google Blocks creation
Import your Sketchup creation
Use teleportation with the example floors and teleportation points
Interact with the MOCAP system

The starter kit is made to be used with Unity version 2017.3 or later.
The following components are part of the Unity VR Starterkit:

* [SteamVR unity plugin](https://assetstore.unity.com/packages/templates/systems/steamvr-plugin-32647)
* [Poly plugin](https://developers.google.com/poly/develop/unity)
  * Note: if you get an error, activate "unsafe" code in Edit -> ProjectSettings -> Player -> Other Settings
* [Unity OSC toolkit](https://github.com/hku-ect/UnityOSCToolkit)
* [Teleportation setup](https://unity3d.college/2017/05/16/steamvr-locomotion-teleportation-movement/)

### How to import your Tiltbrush drawing?
Open Tiltbrush, make your artwork then choose uploadfrom the menu to upload & publish your artwork to your Poly account. Within the Unity project you can now find your created artwork in the Poly Toolkit window when sou sign in with your account under “Your Uploads”

### How to import your Google Blocks creation?
Open Google Blocks, make your artwork then choose save from the menu to save your artwork to your account. Within the Unity project you can now find your created artwork in the Poly Toolkit window when sou sign in with your account under “Your Uploads”

### How to import your Sketchup models?
The easiest way to use your Sketchup model in Unity is to drag your .skp file into Unity. Unity will automatically create a “prefab” that you can add to your scene.

However if you have used textures in your SketchUp model it is best to open your model in Sketchup then go to “File->Export->3D model” and export your model as FBX. 
This will generate a .FB file and a folder with your textures which you can then add to the “Assets -> Sketchup” folder from where you can add the “prefab” to your scene.


# FAQ

**Setting Up Steam Input for Mocap VR Calibration**
Note: You'll need to have a Vive connected for this to work.

* Open Window -> Steam Input
  * Make sure two events exist, or create them if they don't:
    * ClickTriggerLeft
    * ClickTriggerRight
    * Settings for both: Boolean, suggested (should be the default setup)
  * At the top of this list, make sure the mode is set to "per hand" instead of "mirrored" (which is the default)
  * Click "Save & Generate", and wait for it to complete
  * Click the button to the right "Open Binding UI" (a browser window will open)
  * Click "Edit" on the active binding
  * Navigate to the "Trigger Click" on both controllers (left and right), and:
    * Click the edit icon
    * Select the ClickTriggerLeft action for the left controller
    * Select the ClickTriggerRight action for the right controller
  
* Open the "main" scene
  * Navigate to the CameraRig, and select the Controllers
  * Set the Input Action of the "LeftHand" controller to the ClickTriggerLeft event
  * Set the Input Action of the "RightHand" controller to the ClickTriggerRight event

**How do I match the MOCAP space with the VR space ?**

Note: Make sure you have the correct Steam Input setup before attempting to calibrate!

In order to match the VR and Motion Capture spaces, they need to be calibrated. An example scene for this has been added (Scenes/main). What you'll need to perform calibration (according to the setup in this scene):
  - A Motive project, with at least two rigidbodies named "left" and "right" (a third, named "testObject" is optional)
    - These names can be altered, and have been set on the Calibration object, as well as the CalibrationRigidbodies objects (in the scene)
  - A running NatNet2OSCBridge which is sending the data from Motive into Unity

<b>Automatic</b><br>
To calibrate, perform the following actions:
  - Check which controller is "L" and which is "R" (only visible from the Vive headset)
  - Take off the Vive headset
  - Place the "right" rigidbody in the front-right (+x/+z) area of the mocap space (inside the VR volume, doesn't need to be precise, just needs to be well tracked so avoid the outer mocap corners, 1-2 meters from the center is fine)
  - Place the "left" rigidbody in the rear-left (-x/-z) area of the mocap/vr space
  - Click the "R" controller's trigger on top of the "right" rigidbody
  - Click the "L" controller's trigger on top of the "left" rigidbody
  - (Recommended) Put the Vive headset on again, and test how well you've matched by viewing the "testObject" rigidbody, or others you've added

Calibration is stored and automatically re-used upon startup. Sometimes data is corrupted, or you might want to re-do calibration (good idea to do before you start), so you can "invalidate" calibration data by navigating to <i>Resources/calibrationData</i> (select it), and unchecking the checkbox that indicates this data is still valid.

<b>Do not delete this calibrationData object!</b> (if you have, you can make another one by right clicking, and selecting Create -> Calibration Data, then renaming it to "calibrationData")

<b>Manual</b><br>
Make sure that the OSCclient and the Rigidbody children are of the CameraRig then correct the offset by translating the Rigidbody group relative to the CameraRig. Als make sure that the X and Z axis of the SteamVR setup are the same as the MOCAP calibration.


## Useful Links:
https://www.raywenderlich.com/149239/htc-vive-tutorial-unity
