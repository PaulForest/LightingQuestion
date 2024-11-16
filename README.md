# Lighting Question

## The Question

Using 3 points (p1, p2, p3) to represent a surface of a solar panel, and a single Vector3 representing the direction of directional light, how can you compute the intensity of the light that the solar panel receives?  

Assume only one side of the solar panel can receive light.  Assume we're using a directional light, whose position is not used, as opposed to a Spot Light.

## The Solution

Figure out the normal of plane defined by the 3 points, and compute the angle between it and the directional light.  If the angle is between 90 and 180 degrees, the solar panel is lit, with a maximum at 135 degrees.  If the angle is less than 90 degrees, the directional light is aimed at the back of the solar panel, so the intensity is zero.

### Algorithm:

1. Use the 3 points to create two vectors:
   * v1 = p3 - p2
   * v2 = p1 - p2
2. Get the cross product of v1 and v2 to get the normal of the plane they define:
   * normal = v1 X v2
3. Get the angle between the directional light and the normal:
   * angle = Vector3.Angle(normal, lightDirection);
4. If the angle is between 90 and 180, we're pointing at the business surface of the solar panel.  Scale the angle [90, 180] to the range [0, 1]
5. Else set the intensity to 0 (we're pointing at the back of the solar panel)

### Code

See solution in the project in this class:
[MyStuff.SunlightIntensity.GetIntensityOfLightOnSurface](https://github.com/PaulForest/LightingQuestion/blob/main/LightingQuestion/Assets/MyStuff/SunlightIntensity.cs#L78-L78)

### Video of solution:

Here we see several "solar panels" at slightly different angles, and a single directional light that spins in a circle.  Each solar panel should light up from black to yellow based on how much light reaches the solar panel.  

The grey surface of the solar panels is the business end that should recieve light, and the blue end is the back that should not recieve light.  In the center is a sphere whose colour changes based on the computed intensity.  

The red debug lines are the normal to the solar panel, the yellow lines are the directional light, just shown right next to the solar panel for comparison.  

[foo.webm](https://github.com/user-attachments/assets/f8b47bb6-a73b-458d-a0f1-546181505c2a)
