public enum ActivityState
{
    Created, // Not visible.
    Started, // Visible, start fade.
    Resumed, // Become active or restored focus.
    Paused,  // Bocoming inactive or losing focus.
    Finished // Fading out.
}
