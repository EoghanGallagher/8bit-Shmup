using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
	void Register( IObserver o );
	void UnRegister( IObserver o );
	void NotifyObserver(  );
}
