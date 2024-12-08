namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Event handler for a registry change event.
    /// </summary>
    /// <param name="sender"><see cref="object"/></param>
    /// <param name="e"><see cref="RegistryChangeEventArgs"/></param>
    public delegate void RegistryChangedHandler(object sender, RegistryChangeEventArgs e);
}
