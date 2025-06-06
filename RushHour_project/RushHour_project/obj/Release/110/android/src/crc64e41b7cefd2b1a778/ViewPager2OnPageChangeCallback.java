package crc64e41b7cefd2b1a778;


public class ViewPager2OnPageChangeCallback
	extends androidx.viewpager2.widget.ViewPager2.OnPageChangeCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPageSelected:(I)V:GetOnPageSelected_IHandler\n" +
			"";
		mono.android.Runtime.register ("RushHour_project.ViewPager2OnPageChangeCallback, RushHour_project", ViewPager2OnPageChangeCallback.class, __md_methods);
	}


	public ViewPager2OnPageChangeCallback ()
	{
		super ();
		if (getClass () == ViewPager2OnPageChangeCallback.class)
			mono.android.TypeManager.Activate ("RushHour_project.ViewPager2OnPageChangeCallback, RushHour_project", "", this, new java.lang.Object[] {  });
	}

	public ViewPager2OnPageChangeCallback (boolean p0, int p1, android.content.Context p2)
	{
		super ();
		if (getClass () == ViewPager2OnPageChangeCallback.class)
			mono.android.TypeManager.Activate ("RushHour_project.ViewPager2OnPageChangeCallback, RushHour_project", "System.Boolean, mscorlib:System.Int32, mscorlib:Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onPageSelected (int p0)
	{
		n_onPageSelected (p0);
	}

	private native void n_onPageSelected (int p0);

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
