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

**How do I match the MOCAP space with the VR space ?**

Make sure that the OSCclient and the Rigidbody children are of the CameraRig then correct tghe offset by translating the Rigidbody group relative to the CameraRig. Als make sure that the X and Z axis of the SteamVR setup are the same as the MOCAP calibration.


## Useful Links:
https://www.raywenderlich.com/149239/htc-vive-tutorial-unity
