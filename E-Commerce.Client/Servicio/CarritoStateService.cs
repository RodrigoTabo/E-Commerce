namespace E_Commerce.Client.Servicio
{
    public class CarritoStateService
    {

        public event Action? OnChange;

        public void SetMensaje()
        {
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
