# UnitySnippets

Random useful unity tools and mechanics

##### Last modified using Unity 2017.4 LTS

## Q-Routines

A simple system for evaluating coroutines in a specific order.

To use it, you need a `QRoutineController` component on an object. Then, instead of using `MonoBehaviour.StartCoroutine()`, you would use `QRoutineController.Enqueue()`. Everything before the first yield will be evaluated immediately. The rest of the coroutine will be evaluated as standard once all the coroutines in the queue before it have fully evaluated.

The controller can be placed on a parent object and then referenced in its children.

The demo available at *UnitySnippets/QRoutines/Example* uses Q-Routines for the actions of moving the UI elements and setting their colour.

This system was developed as a prototype for a mobile card duelling game to allow card effects to be animated and played out in sequence, but it has a wide range of applications.