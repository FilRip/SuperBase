# SuperBase
Call parent method virtual/override and choice from which class (regardless overrides)<br><br>

Example : <br>
class1 with virtual method Test<br>
class2 inherit class1, override method Test<br>
class3 inherit class2, override method Test<br><br>

in class3, you can call base.Test that will call method Test of class2<br><br>

With this ExtensionMethod, you can choice to call at the start class you want<br>

Example in this case :<br>
in class3, you can call SuperBase<class1> to call Test method of class1 without call Test method of class2, with just one line of code, like base<br><br>

See samples
