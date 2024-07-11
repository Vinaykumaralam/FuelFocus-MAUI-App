package crc6437e00fef79382642;


public class Function1Impl_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		kotlin.jvm.functions.Function1,
		kotlin.Function
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_invoke:(Ljava/lang/Object;)Ljava/lang/Object;:GetInvoke_Ljava_lang_Object_Handler:Kotlin.Jvm.Functions.IFunction1Invoker, Xamarin.Kotlin.StdLib\n" +
			"";
		mono.android.Runtime.register ("The49.Maui.ContextMenu.Function1Impl`1, The49.Maui.ContextMenu", Function1Impl_1.class, __md_methods);
	}


	public Function1Impl_1 ()
	{
		super ();
		if (getClass () == Function1Impl_1.class) {
			mono.android.TypeManager.Activate ("The49.Maui.ContextMenu.Function1Impl`1, The49.Maui.ContextMenu", "", this, new java.lang.Object[] {  });
		}
	}


	public java.lang.Object invoke (java.lang.Object p0)
	{
		return n_invoke (p0);
	}

	private native java.lang.Object n_invoke (java.lang.Object p0);

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
