using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MultiConditionalBoolean<T> where T : Enum {

	private int _value;
	
	public MultiConditionalBoolean() {
		_value = 0;
	}
	
	// Sets the bit
	// Eg: If _value is 0, consider the first 4 rightmost bits 0000
	// We left shit 1 by the int value of the enum, say its 0, so we get 0001
	// Then we do a binary OR, and we get the following
	// 0000
	// 0001
	// ----
	// 0001 --> value after the OR operation, we have set the 1st bit
	// All the other bits are unaffected
	public void Set(T enumValue) {
		try {
			int enumIntValue = (int) Convert.ChangeType(enumValue, typeof(int));
			_value = _value | (1 << enumIntValue);
		} catch (InvalidCastException e) {
			Debug.Log("Cant convert enum to int");
		}
	}
	
	// Resets the bit
	// Eg: If _value is 3, consider the first 4 rightmost bits 0011
	// We left shit 1 by the int value of the enum, say its 0, so we get 0001
	// Then we get the complement of this which gives us, ~(0001) = 1110
	// Then we do a binary AND, and we get the following
	// 0011
	// 1110
	// ----
	// 0010 --> _value after the AND operation, we have reset the 1st bit
	// All other bits are unaffected
	public void Reset(T enumValue) {
		try {
			int enumIntValue = (int) Convert.ChangeType(enumValue, typeof(int));
			_value = _value & ~(1 << enumIntValue);
		} catch(InvalidCastException e) {
			Debug.Log("Cant convert enum to int");
		}
	}
	
	// If value is 0, then its false
	// else true
	public bool IsTrue() {
		return _value != 0;
	}
	
	public void Clear() {
		_value = 0;
	}
}
