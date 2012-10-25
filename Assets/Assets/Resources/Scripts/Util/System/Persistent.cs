using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
public class Persistent :Attribute
{
	public string name;
	public bool updateOnInstantiation;
	
	public Persistent(string name="",bool updateOnInstantiation=false)
	{
		this.name=name;
		this.updateOnInstantiation=updateOnInstantiation;
	}
}


