# Debugger

**CSE 143: Computer Programming II Spring 2020**

![](RackMultipart20200527-4-64mi9g_html_7d7e44fa345f092f.gif)

**Section Project: Game Development in Unity: Debugger**

**This assignment is meant as an extension past the material taught in CSE 143 and teaches the basics of game development and managing large-scale projects. There is no due date, it is for your personal development as a programmer. Base code written by Jonah Cortezzo, Thomas Hsu, Nguyen Duong, and Price Ludwig. Base code references scripts from Six Dot, whose repository can be found** [**here**](https://github.com/wbeaty/NuclearThronesCamera) **.**

You will need Unity real-time development platform for this project. You can download Unity for free [here](https://store.unity.com/#plans-individual). You will also need to download the starter code from our GitHub repository which you can find [here](https://github.com/jcortezzo/Debugger). In order to edit files, you will need to make sure Visual Studio is installed. This should happen automatically when you install Unity, just make sure you don&#39;t uncheck it when it&#39;s trying to install.

**Understanding the Basics of Unity**

Unity has a lot of different aspects that are vital to understand for general game development. While a lot of things we will go over are Unity specific, you can find parallels in other game engines if you choose to work with those in the future. The first major difference is while we have spent CSE 142 / 143 working in Java, Unity uses C# as its scripting language. C# and Java are extremely similar, so the basic approach you should take to writing code in C# should be to attempt it in Java, then simply look up lines that don&#39;t seem to be working so you can translate them to C#.

Scenes

![](RackMultipart20200527-4-64mi9g_html_265b594bd05a97a.png)

Scenes are where your game comes together. Each scene holds the game objects that are required for each part of your game - the game objects appear on the left &quot;Hierarchy,&quot; and in the editor and inspector, you&#39;re able to manipulate these game objects as you please. Scenes separate the different parts of your game - it would be weird to have the objects for your main menu in the same place as the actual game itself!

Object-Oriented Programming (OOP) in Unity: GameObjects and Prefabs

C# is still an object-oriented language, meaning you can use all the same OOP features we&#39;ve seen in CSE 14x. Unity, however, uses an even higher level of OOP, using **Prefabs** and **GameObjects**. A GameObject is the base class for all entities in a Unity Scene (which can be thought of essentially as a &quot;level&quot;). A GameObject is composed of multiple **Components** that define its behavior. A Component is simply anything that defines the behavior of a GameObject, so includes everything from scripts, to collision boxes, to physics / particle simulators. On the other hand, a **Prefab** is a blueprint for a GameObject. In a way it is similar to [classes versus objects in Java](https://www.javatpoint.com/difference-between-object-and-class). In making games, you will want to make Prefabs of various entities such as enemies, players, weapons, and items, and then you will use those Prefabs to make GameObjects in a Unity Scene. This is the high-level overview of how to create a level.

Essential Components

Unity has a large number of components, but the good news is you won&#39;t have to use all of them. There are, however, a handful of important components that are most commonly used. We will discuss those most common components in this section, though you are encouraged to do your own research and find other components that you think may be useful!

**Transform**

![](RackMultipart20200527-4-64mi9g_html_61661b326fd33a40.png)

Every Unity GameObject is required to have a Transform component. Transforms describe the &quot;worldly properties&quot; of a GameObject, those being the position, rotation, and scale of the object. These properties are relative to the GameObject&#39;s parent, meaning that if your GameObject is a child of another, x=0,y=0,z=0 means your object will appear right in the center of its parent. If the child-parent relationship makes no sense to you here, don&#39;t worry, we&#39;ll cover that in more depth later!

**RigidBody2D**

![](RackMultipart20200527-4-64mi9g_html_acac4a896ecf48c8.png)

RigidBodies handle physics in the Unity engine. Specifically, they determine an object&#39;s position based on the engine&#39;s own physics simulations. One nice part about unity is it comes with a built-in physics engine that you can choose to use, so if you want to use something like gravity or apply force to a moving object, the engine will do all the calculations for you! One part that may be confusing is that both Transforms and RigidBodies handle position. The difference is Transforms do not consider physics, so if you choose to move something by Transform rather than RigidBody, additional forces like gravity will not affect the movement. You can think of Transforms as teleporting entities by small amounts to move and RigidBodies as actually simulating real world movement. This doesn&#39;t make one worse than the other, you can use them both together or separately for different reasons. Also watch out! When you add a RigidBody2D component to a GameObject or Prefab, gravity by default will be set to 1. Change it to 0 if you don&#39;t want any gravity!

**Collider2D**

![](RackMultipart20200527-4-64mi9g_html_130475467b52aa3f.png) ![](RackMultipart20200527-4-64mi9g_html_e73c3d93f93aeea0.png)

Colliders handle collisions in Unity, that is, the conditions in which two objects are colliding. When two GameObjects&#39; Colliders hit each other, a collision is made. The Collider does not have to match up with the sprite, it can be arbitrarily different. In the example above, we can see that Kevin&#39;s Collider is much smaller than his sprite, meaning in this game that things won&#39;t hit him unless they hit his torso area. Kevin&#39;s hair is safe from harm! Note that there are different kinds of colliders. The two most basic versions are Box and Circle Colliders. You also have the option, however, to check the &quot;Is Trigger&quot; box, which will change the collider from a physical aspect to just a &quot;trigger.&quot; What this means is if something &quot;hits&quot; the box, it won&#39;t actually interact with physics, so it won&#39;t stop and will just go through you. It will however cause a trigger event to happen; we will discuss how to make use of those later. Another important thing to consider is that more often than not, every object with a Collider2D should have a RigidBody2D. Static objects, i.e. objects that will never move themselves, are exempt from this, but if you want an object to move it needs a RigidBody2D so it can work with Unity&#39;s built-in physics engine.

**SpriteRenderer**

![](RackMultipart20200527-4-64mi9g_html_5544d9fee3ed64f4.png)

SpriteRenderers are simple, they simply display a sprite for the GameObject both in the Scene Editor and in game. The SpriteRenderer has some useful fields to it, such as Color and Flip which can be used to change the Color of the sprite and mirror it.

**Animator**

![](RackMultipart20200527-4-64mi9g_html_4ad631d94bce9442.png)

Animators handle sprite animation of the GameObject. Animators have a Controller and various animation &quot;clips.&quot; The Animator&#39;s Controller controls which clip plays when. Animators will be discussed later on in more detail.

Scripts and Essential Methods

Writing scripts to determine the behavior of each game object is also an important part of making a game. Scripts are the final essential component and are very similar to the Java files we&#39;ve spent time writing in CSE 14x. When writing a new script in Unity, you can see that each class inherits from a Monobehaviour, which is the parent class for all scripts that can be run inside Unity. This is similar to how each Critter needs to extend a parent Critter class. Below are the most common methods that you&#39;ll have to work with that come from this parent class.

Methods To Implement

**Start()**

This method determines what is being run at the first frame. Think of this as the constructor where you initialize all the fields and get the components of the objects you need to use.

**Update()**

This method controls what&#39;s being run at each frame. A frame is essentially a snapshot of a game at a specific point in time, much like how a movie is composed of multiple images played in rapid succession. Use this method for updating parts of the game that need to feel responsive. This includes receiving input, updating animations, and using timers.

**FixedUpdate()**

This method is like Update, but is synchronized to framemate. Use this method for Rigidbody2D physics manipulations. The reason for this is that we don&#39;t want the physics code to run twice as much on a computer that&#39;s twice as fast, as this might make the game speed up depending on how many frames the game is running. If you&#39;ve ever emulated games you can see the effects of this; games that tie physics to frame rates can run into a ton of issues when put on machines with different performance rates.

**OnCollision(Enter/Exit/Stay)2D(Collision2D)**

Determines what happens when two colliders that are **not** triggers hit each other. Enter will be called once when the objects first touch, exit will be called once when the objects finish touching, and stay will be called on every physics frame that the objects are touching (think FixedUpdate()).

**OnTrigger(Enter/Exit/Stay)2D(Collider2D)**

Works the same as OnCollision(Enter/Exit/Stay)2D() but only with Colliders that are triggers (the trigger boolean is set to true). This is useful if you don&#39;t want two objects to interact with the physics of each other but do want to know when they&#39;re touching.

Methods To Use

**GetComponent\&lt;E\&gt;()**

GetComponent\&lt;E\&gt;() is a method that you&#39;ll use in other methods to get components associated with the GameObject you&#39;re working within. For example, if you want to modify the rigidbody&#39;s velocity of the GameObject within a script, you&#39;ll first need access to the RigidBody2D. You can do this with a simple call as such.

RigidBody2d rb = GetComponent\&lt;RigidBody2D\&gt;();

rb will now be assigned to the GameObject&#39;s RigidBody2D (the same GameObject that is using the current script as a component). If the current GameObject does not have type E (in this instance a RigidBody2D), GetComponent\&lt;E\&gt;() will return null.

**Destroy(Object, float = 0.0f) and OnDestroy()**

This is another method that you won&#39;t implement but will use in other public methods. This removes the given object. There&#39;s an optional parameter that represents the number of seconds to wait before the object is destroyed. Used, for example, when an enemy dies and should be removed from the game. If you want to Destroy() yourself, you would call Destroy(this.gameObject). There is also a helpful method OnDestroy() that you _can_ implement that defines behavior of what happens right before Destroy() is called.

**Instantiate(Object)**

Similar to Destroy(), but for creating objects. Used when spawning bullets out of the gun when a player shoots, for example. Takes in additional arguments that determine where the object is spawned—[check out the documentation on the official Unity page!](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html)

**Debug.Log() and print()**

Prints out stuff to the Unity console! Helpful for debugging as it&#39;s just like System.out.println().

![](RackMultipart20200527-4-64mi9g_html_7d7e44fa345f092f.gif)

**Development Guidelines**

While the game we have is mostly complete, it&#39;s only a very basic prototype of basic mechanics. We want to be able to iterate on this prototype and add more content. The base code is modularized enough that you don&#39;t need to understand exactly how each class works, but you&#39;re encouraged to look through the various relevant files to garner a better understanding of the things you can do. You&#39;ll notice comments scattered across various scripts that read &quot;TODO:&quot;. These sorts of comments are commonly used in large-scale projects where skeleton code is provided so you know what places you need to edit / implement.

Player

So far Kevin is sitting in a scene surrounded by bugs, but he can&#39;t outrun them. In fact, Kevin cannot move at all! For this part of the assignment,you will **implement player movement**.

You&#39;ll notice that Player.cs extends LivingEntity.cs. LivingEntity is an abstract class that is a superclass to both Players and Enemies since the two subclasses share a lot of commonalities (health, movement speed, behavior definition for attacking and moving, etc.). Move() is an abstract method of LivingEntity, meaning that both Player and Enemy must implement the method in their own ways. Player.Move() is where you will write code to implement movement. As discussed in the components section, there are a few ways you can implement movement in Unity. For this assignment, please implement movement by modifying the velocity field of Player&#39;s RigidBody2D. The Player&#39;s RigidBody2D is an inherited field from LivingEntity (you may want to look through LivingEntity to get an idea of what fields you have in Player!). The velocity field is of type Vector2 which takes in an x and y component representing horizontal and vertical direction. To modify this, you will have to find a way to assign these values to the velocity Vector2. After making the Vector2, make sure to normalize the vector and multiply it by the speed field to make sure the player moves at the same speed in all directions.

Hint: Look online for ideas of how to get a horizontal and vertical speed! This part of the project is meant to get you used to looking up Unity documentation. A million games have been created, so a simple Google search should point you to a bunch of places where you can find the right answer!

Note: You do not necessarily need to have a strong basis in linear algebra to create games, but it&#39;s definitely helpful. If you find yourself not understanding vector math fully that&#39;s okay, this project will guide you through all game-specific math! To give some basic understanding of vectors, a vector represents a direction in either a 2D or 3D space (Vector2 vs Vector3). Point a - Point b will create a vector that originates at Point b and points to Point a. Vectors have lengths, but we only care about direction in this case, so we want it to have a length of one. This is what normalizing a vector does, so it is something we want here. We don&#39;t want a player to move faster when walking one direction as opposed to another, so we normalize the vector and then multiply it by a speed to ensure every vector has the same length.

Enemies

So far Kevin is only being chased by one basic bug in each level. As we all know after taking intro CSE, having only one type of simple bug is not the reality of programming. In order to diversify our game and make it closer to reality, we will be implementing more enemy bugs in this part of the assignment.

**Intermediate Bug**

The first basic bug in the game always moves towards Kevin, so it&#39;s easy for him to anticipate these bugs and get them out of his code. Real bugs, however, are a lot trickier to track. For the **Intermediate Bug** , we will create a new Enemy that exhibits less predictable behavior. To start, duplicate the Bug Prefab in Assets/Prefabs/Enemies/ and rename the new copy IntermediateBug. Then, create a new script in Assets/Scripts/Enemies/ named IntermediateBug.cs. Make sure IntermediateBug extends Enemy. In the IntermediateBug prefab, remove the Bug (Script) component and scroll to the bottom of the components list and click on &quot;Add Component.&quot; If you type in IntermediateBug, you should see the script you just made pop up. Click on the script that you just searched for to add it as a component to your IntermediateBug Prefab. Congratulations! You&#39;ve just created a new Enemy! You&#39;ll find that it&#39;s still pretty basic, so we&#39;re about to change that!

| **Move()** |
| --- |
| The basic bug always moves towards Kevin. We want the intermediate bug to be a bit trickier, moving towards Kevin and then moving away to bait him. Specifically, the **intermediate bug should move towards Kevin for four seconds, then move directly away from Kevin for two seconds**. You will have to create a timer for this as a field. Time.deltaTime will return the time it&#39;s been since the last frame, so making a field timer that&#39;s set to value five for example and subtracting Time.deltaTime in update will tell you that the five second timer has expired when its value is \&lt;= 0. |

| **IsAttacking()** |
| --- |
| When attacking, allow the **intermediate bug to shoot breakpoints at the player**. To do this, you can give it a Step gun (one of the weapons in the game) for the bug to attack with. Enemies and Players are both LivingEntities, so they can both hold and use weapons. You may notice how we implemented attacking in the basic bug by just giving the bug a keyboard weapon, making the keyboard invisible by disabling (unchecking) its SpriteRenderer, and then changing the hitbox of the invisible keyboard to give the basic bug the desired range we want it to have. You can employ a similar strategy to give a bug the ability to shoot breakpoints. The method that determines the conditions when a LivingEntity will attack is the IsAttacking() method. IsAttacking() returns true if the LivingEntity should attack and false if it shouldn&#39;t. The default behavior for an enemy to attack is when it is within a certain field attackingDistance of its target (which for now is always the Player). The intermediate bug should start shooting at the Player if the Player is within six units of the intermediate bug. |

**Advanced Bug**

| **IsAttacking()** |
| --- |
| You may have noticed that it&#39;s a bit unfair for the intermediate bug to constantly shoot at the player. As game designers it&#39;s not our job to try to beat the player, but rather to give the player a challenge that they can overcome but noticing things about the game like enemy attack patterns. Realism isn&#39;t as important as the fun factor for most games. To make this a more fair fight, we&#39;ll give the Advanced Bug a &quot;cooldown&quot; on its attack pattern. The advanced bug should start shooting at the player when the player is within six units of the bug, but it should only shoot for a maximum of three seconds until it gets tired. After three seconds, the advanced bug should stop shooting and should have to wait another three seconds before it again has the ability to start shooting. |

| **Animator** |
| --- |
| The advanced bug now behaves a bit better than the other two bugs, but we should distinguish it from the others by giving it its own unique sprite and animations. In order to do this, we need to edit the bug&#39;s **Animator**. |

Weapons

Right now, there are only two basic weapons: a pistol (animated to look like a part of jGrasp&#39;s debugger) and a melee weapon (keyboard). However, the bugs that are implemented are some nasty ones, and might require a larger arsenal of debuggers to take care of these nasty critters! Here are some weapons for you to implement.

**Print Debugger**

\&lt;image of weapon here\&gt;

One of the most helpful debugging tools is the mystical &quot;System.out.println()&quot; command. Once Kevin gets a hold of this, no bug will be safe! This print debugger is capable of shooting random phrases and words to decimate anything that comes in its path. Implement a print debugger that does [thing]

Projectiles

Bells and Whistles

The coolest part about big projects like this is you can keep working even past what&#39;s required to put your own ideas and creativity to use! It is highly recommended for you to implement at least a couple of Bells and Whistles to spice up your game. Here are some recommended features that you can implement past this basic spec:

- Create a weapon that has custom animations. This will require using an Animator to determine what sprite is shown on the weapon at what time. A basic idea for a weapon is a shooter that can be charged by holding attack. When the charge is ready, you can change the sprite of the weapon using the Animator to show that it&#39;s charged.
- Create a projectile that moves in a sinusoidal fashion. That is, the projectile should move like a wave. This will require mostly geometric reasoning, so if you don&#39;t want to dive into more math you may want to skip this bell. You can use this for reference:

![](RackMultipart20200527-4-64mi9g_html_90bf30f216bcc6fc.png)

- Create a weapon that spawns enemies that fight for **you**. These &quot;friendly enemies&quot; should attack other enemies. For this, you may want to look at the Alignments.cs enum and the code in Enemy.cs that determines how Enemies pick targets to attack.
