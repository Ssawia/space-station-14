using Content.Server.Censored;
using Content.Shared.Actions;



namespace Content.Shared.Censored;
public abstract class SharedCensoredSystem : EntitySystem
{


    public override void Initialize()
    {
        SubscribeLocalEvent<CensoredComponent, ComponentStartup>(OnStartup);

    }

    private void OnStartup(EntityUid uid, CensoredComponent component, ComponentStartup args)
    {
        if (!TryComp(uid, out ActionsComponent? comp))
            return;

    }

}
