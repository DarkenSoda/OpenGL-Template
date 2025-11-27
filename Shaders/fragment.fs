#version 330 core

in vec3 ourColor;

uniform float time;

void main()
{
    vec3 timedColor = vec3(sin(time), cos(time), sin(time));
    timedColor = timedColor * 0.5 + 0.5; // Normalize to [0, 1]
    vec3 color = ourColor * timedColor;

    gl_FragColor = vec4(color, 1.0f);
}