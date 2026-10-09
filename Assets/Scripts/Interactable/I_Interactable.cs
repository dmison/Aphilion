namespace Interactable
{
    public interface IInteractable
    {
        public string GetLabel();
        public bool InterationActive();
        public void Interact();
        
    }
}