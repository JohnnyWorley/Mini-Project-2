Mini-Project 2: Raycaster

This project was made to test my knowledge after I saw a YouTube video that created a similar project and I wanted to see if I could do it. It is similar to how some Retro games such as DOOM would render their environments using a 2D map and a Raycaster, however,r this small project doesn't do any map building and is only a Raycaster. I also draw triangles between all the points to give an effect of the areas visible by the raycast being lit. I also use vertex colours for the triangles however as you can see in the provided screenshots the triangles colours don't blend well together that well which looks a bit funky.

![image](https://github.com/user-attachments/assets/ef1bbdc9-1dff-4009-964c-0d58dd6fab91)

It also performs very well even with 360 Raycasts happening each frame, originally it did run very poorly but then I optimized it using Object Pooling with the drawn triangles.

I also added Gizmos so you can see where each individual Raycast hits.

![image](https://github.com/user-attachments/assets/c4c08404-847e-4484-b962-1d29af02f8d4)

To add more to this I added the ability for the light to reveal certain objects. To do this I created a shader that had its Depth Test set to "GEqual", this means that the GameObjects with the material on would only be rendered if they had something being Rendered in front of them, blocking the camera.

![image](https://github.com/user-attachments/assets/1baaa5cb-c4bd-456c-9ad3-6376de899d84)

