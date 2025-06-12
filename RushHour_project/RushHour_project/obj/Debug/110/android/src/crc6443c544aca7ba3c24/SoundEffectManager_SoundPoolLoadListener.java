package crc6443c544aca7ba3c24;


public class SoundEffectManager_SoundPoolLoadListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.media.SoundPool.OnLoadCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLoadComplete:(Landroid/media/SoundPool;II)V:GetOnLoadComplete_Landroid_media_SoundPool_IIHandler:Android.Media.SoundPool/IOnLoadCompleteListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("RushHour_project.Sounds.SoundEffectManager+SoundPoolLoadListener, RushHour_project", SoundEffectManager_SoundPoolLoadListener.class, __md_methods);
	}


	public SoundEffectManager_SoundPoolLoadListener ()
	{
		super ();
		if (getClass () == SoundEffectManager_SoundPoolLoadListener.class)
			mono.android.TypeManager.Activate ("RushHour_project.Sounds.SoundEffectManager+SoundPoolLoadListener, RushHour_project", "", this, new java.lang.Object[] {  });
	}


	public void onLoadComplete (android.media.SoundPool p0, int p1, int p2)
	{
		n_onLoadComplete (p0, p1, p2);
	}

	private native void n_onLoadComplete (android.media.SoundPool p0, int p1, int p2);

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
