package crc6437e00fef79382642;


public class ContextMenuSnapshotPreview
	extends crc6437e00fef79382642.ContextMenuPreview
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.ContextMenuSnapshotPreview, The49.Maui.ContextMenu", ContextMenuSnapshotPreview.class, __md_methods);
	}


	public ContextMenuSnapshotPreview (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ContextMenuSnapshotPreview.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuSnapshotPreview, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ContextMenuSnapshotPreview (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ContextMenuSnapshotPreview.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuSnapshotPreview, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ContextMenuSnapshotPreview (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ContextMenuSnapshotPreview.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuSnapshotPreview, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public ContextMenuSnapshotPreview (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ContextMenuSnapshotPreview.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.ContextMenuSnapshotPreview, The49.Maui.ContextMenu", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);

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
