package crc6487c8b7f220a08c52;


public class Level
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("RushHour_project.Classes.Level, RushHour_project", Level.class, __md_methods);
	}


	public Level ()
	{
		super ();
		if (getClass () == Level.class)
			mono.android.TypeManager.Activate ("RushHour_project.Classes.Level, RushHour_project", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
