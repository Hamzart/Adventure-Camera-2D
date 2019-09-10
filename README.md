# Adventure Camera 2D for Unity

Multipurpose modular 2D camera for Unity, can be used out of the box in different game genres, no coding required unless you want to extend the functionalities.


# Behaviors :

***Version 0.2***

- Follow  :the Mouse pointer or a GameObject like your Player.
- Limiter : Region for the camera, Camera will not leave the limiter Bounds even if the Target leave.
- DeadZone : to prevent the camera from moving while the Target is inside the DeadZone area.
- Look Ahead : to make the camera detect the target direction and Move ahead of it by a defined distance:
    - Detect by Rotation on the Y axis.
    - Detect by Scale on the X axis, if the Target Scale on the X axis is less then 0 or higher than 0.
    - Detect by Velocity ( Untested yet).

- Camera Zoom : ability to smoothly zoom smoothly to a certain size.
- Offset : set an offset between the Camera and the Target to follow.


# How To use?

1. Select a Camera > Add Component > Adventure Camera 2D > **Core**.

Thats it, now your Camera will be functioning like charm, you do not even need to switch it to Orthogonal projection, **CameraCore** will take care of everything. **CameraCore** is the base of the **2D Adventure Aamera**, any other module depends on the CoreCamera.
You do not even need to create a Camera in the first place, Select any Object in the Scene add CameraCore to it, and you will have a CameraThere. 

Now You can Add other Modules to entend the **CameraCore** functionalities.
There 3 Separate Modules currently: 

  - DeadZone
  - Limiter
  - Zooming


# TO DO List :

More Features should be added in the future including :

- Camera Shake :
- Parallax Group :
- Auto Zoom Zone :
- ...etc
