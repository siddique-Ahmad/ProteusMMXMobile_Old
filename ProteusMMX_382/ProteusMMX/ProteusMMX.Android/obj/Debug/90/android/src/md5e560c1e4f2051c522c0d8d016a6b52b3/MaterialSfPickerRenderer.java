package md5e560c1e4f2051c522c0d8d016a6b52b3;


public class MaterialSfPickerRenderer
	extends md5e560c1e4f2051c522c0d8d016a6b52b3.SfPickerRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.SfPicker.XForms.Droid.MaterialSfPickerRenderer, Syncfusion.SfPicker.XForms.Android", MaterialSfPickerRenderer.class, __md_methods);
	}


	public MaterialSfPickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MaterialSfPickerRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfPicker.XForms.Droid.MaterialSfPickerRenderer, Syncfusion.SfPicker.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MaterialSfPickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MaterialSfPickerRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfPicker.XForms.Droid.MaterialSfPickerRenderer, Syncfusion.SfPicker.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MaterialSfPickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MaterialSfPickerRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfPicker.XForms.Droid.MaterialSfPickerRenderer, Syncfusion.SfPicker.XForms.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
