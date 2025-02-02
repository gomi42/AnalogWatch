# Analog Clock

This project implements an analog clock control. The appearence of the clock is highly configurable by a XAML style. The movement of the secand hand support three different modes: **Jump**, **Smooth**, **Continous**.

The control includes 10 clock face implementations. The last one it a test that implements every single feature in a single clock face.

When you implement your own clock face keep in mind that

- hour ticks
- minute ticks
- second ticks
- hour hand
- minute hand
- second hand

need to be defined along the full diameter (not radius). The center of these elements is located at the center (rotation point) of the clock face. That gives you more freedom to define special hands.



![clockfaces](/ClockFaces.jpg)
