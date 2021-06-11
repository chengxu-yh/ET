namespace ET
{
    public static class OperationerComponentSystem
    {
        public static bool IsOperationer(DUnit unit)
        {
            if (unit.GetComponent<OperationerComponent>().OperationerId == unit.Domain.GetComponent<GamerComponent>().myGamer.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}