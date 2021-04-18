# ObjectFlow
ObjectFlow flows the set of objects it creates towards the specified target.

*This package is not dependent on any additional libraries.*

![Showcase](Documentation/showcase.gif)

# Installation
1. Copy `https://github.com/Gnarly-Games/ObjectFlow`
2. Open package manager with following `Window -> Package Manager`
3. Click **Add package from git URL** and paste the URL.
![Installation Step 1](Documentation/installation_step_1.png)
![Installation Step 2](Documentation/installation_step_2.png)
![Installation Step 3](Documentation/installation_step_3.png)

# How To Use
1. Create a child canvas within your canvas.
2. Create a GameObject within the child canvas.
3. Add `ObjectFlow` script to the `GameObject`.
4. Define projectile amount and speed.
5. Define explosion radius and speed. Tweak the Animation Curves of the explosion.
6. Assign the **target** `GameObject` and tweak the Animation Curves of the **flow path**.
7. Invoke the `ObjectFlow.Flow()` method.
![Demo](Documentation/demo_screenshot.png)

# Demo Video
If you have trouble using the package, try to follow the steps by watching the demo video. 
![Demo](Documentation/tutorial.gif)

# License
`ObjectFlow` is offered under the Apache 2 license.