namespace ET
{
    public static class NumericAction
    {
        public static void SetUnitNumericAction(DUnit unit, NumericType numeric, int val)
        {
            if (unit.DomainScene().GetComponent<PVPComponent>().bePVP)
            {
                if (OperationerComponentSystem.IsOperationer(unit) == false)
                {
                    return;
                }

                C2M_UnitNumeric msg = new C2M_UnitNumeric();
                msg.Id = unit.Id;
                msg.BeInt = true;
                msg.NumericType = (int)numeric;
                msg.Val = val;

                unit.Domain.GetComponent<SessionComponent>().Session.Send(msg);
            }
            else
            {
                NumericAction.SetUnitNumericActionImp(unit, numeric, val);
            }
        }

        public static void SetUnitNumericAction(DUnit unit, NumericType numeric, float val)
        {
            if (unit.DomainScene().GetComponent<PVPComponent>().bePVP)
            {
                C2M_UnitNumeric msg = new C2M_UnitNumeric();
                msg.Id = unit.Id;
                msg.BeInt = false;
                msg.NumericType = (int)numeric;
                msg.Val = val;

                unit.Domain.GetComponent<SessionComponent>().Session.Send(msg);
            }
            else
            {
                NumericAction.SetUnitNumericActionImp(unit, numeric, val);
            }
        }

        public static void SetUnitNumericActionImp(DUnit unit, NumericType numeric, int val)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            numericComponent.Set(numeric, val);
        }

        public static void SetUnitNumericActionImp(DUnit unit, NumericType numeric, float val)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            numericComponent.Set(numeric, val);
        }
    }
}