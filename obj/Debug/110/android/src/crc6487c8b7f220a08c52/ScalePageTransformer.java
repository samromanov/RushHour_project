package crc6487c8b7f220a08c52;


public class ScalePageTransformer
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.viewpager2.widget.ViewPager2.PageTransformer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_transformPage:(Landroid/view/View;F)V:GetTransformPage_Landroid_view_View_FHandler:AndroidX.ViewPager2.Widget.ViewPager2/IPageTransformerInvoker, Xamarin.AndroidX.ViewPager2\n" +
			"";
		mono.android.Runtime.register ("RushHour_project.Classes.ScalePageTransformer, RushHour_project", ScalePageTransformer.class, __md_methods);
	}


	public ScalePageTransformer ()
	{
		super ();
		if (getClass () == ScalePageTransformer.class)
			mono.android.TypeManager.Activate ("RushHour_project.Classes.ScalePageTransformer, RushHour_project", "", this, new java.lang.Object[] {  });
	}


	public void transformPage (android.view.View p0, float p1)
	{
		n_transformPage (p0, p1);
	}

	private native void n_transformPage (android.view.View p0, float p1);

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
