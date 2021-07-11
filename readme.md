RandomValues - Notepad++ plugin
===============================

RandomValues is a plug-in for Notepad++ for generating random values for database and app development, test data, passwords etc.

![preview screenshot](/randomvalues_preview.png?raw=true "RandomValues plug-in preview")

RandomValues is based on a prototype project [randomdata](http://bdrgames.nl/homepage/files/randomdata.html)

How to use it
-------------

* Select the menu items to generate a single random value
* select "Generate random values" to generate many random values at once.

You can configure the random value menu items, set the first item to your prefered random value (password, guid etc).
Clicking the dice icon in the workbar will generate yeild a new random value.

`NOTE: changing the configuration of the menu items requires a restart of Notepad++`

How to install
--------------
The distributed output file is `RandomValues.dll`. In your \Notepad++\plugins\ folder,
create a new folder `RandomValues` and place the .dll file there, so:

* copy the file [.\RandomValuesNppPlugin\bin\Release\RandomValues.dll](/RandomValuesNppPlugin/bin/Release/)
* to new folder .\Program Files (x86)\Notepad++\plugins\RandomValues\RandomValues.dll

For the 64-bit version it is the same, except the output file is in the
[Release-x64](/RandomValuesNppPlugin/bin/Release-x64/) folder and Notepad++ is
in the `.\Program Files\Notepad++\` folder.

Masks
----------

Examples for String text values.


Examples for String text values.

    mask character
    --------------------------------
    A  random vowel (AEIOU)
    B  random consonants (BCDFGHJKLMNPQRSTVWXYZ)
    9  random digit (0123456789)
    H  random hex digit (0123456789ABCDEF)
    @  random symbol (!@#$%^&*+-)
    X  random letter (vowel or consonants)
    Y  random letter or digit
    Z  random letter or digit or symbol

Some examples of using a mask to generate random text values (not case sensitive):

    mask               remark
    --------------------------------
    ababab99       generate "exakir97", "ucatuj24" etc.
    xxxyyyzz99     passwords


Note, for generating passwords there are additional options in the generate random values screen.
Enable `Mix mask` to randomize the order of the mask characters for each generated value.
Enable `Password safe char` and the resulting values won't include any `i, o, g, l, s, z, 0, 1, 2, 5, 9` characters.
Change the `case` option to lower-case, upper-case, mixed-case (random) or InitCap, the last will capitalis first letter.

If no mask is configured for a String value, it will generate random snippets from the Lorem Ipsum text.

Ranges
------
Example integer values 

    {1..20}            Random integer from 1 up to and including 20
    {100..999}         Random integer from 100 up to and including 999
    {1,2,3,4,5,6}      Random value between 1 and 6

Example date values

    {2021..2022}                Random date value between 2021-01-01 up to and including 2022-12-31
    {2021-06..2022-04}          Random date value between 2021-06-01 up to and including 2022-04-30
    {2021-06-01..2021-07-01}    Random date value between 2021-06-01 up to and including 2021-07-01
    {2021..2021}                Random date value between 2021-01-01 up to and including 2021-12-31

Example string values

    {M,F}              Random character M or F
    {Low,Medium,High}  Random code value "Low", "Medium" or "High"
    {0,1,9}            Random value 0, 1 or 9

Roadmap/goals
-------------
The RandomValues plugin is still work-in-progress, here is list of features I want to add (~~strikethrough~~ is done)

* ~~random values based on masks~~
* ~~generate single random value~~
* ~~generate multiple values at once~~
* ~~different output formats, csv, SQL, xml, json etc.~~
* support word lists in settings
* ~~additional mask options uppercase/mixed case (for passwords)~~
* ~~option; limit to min max values~~
* option; take x values from a set of values
* output xml, also path support? @properties etc. maybe?
* replace values in csv format, replace only in column X
* replace regex? is this needed? can also use default Notepad++ regex search + generate value

Trouble shoot and tips
----------------------
When generating passwords, note that generating lots of values will not guarantee that all the passwords are unique.

You can use the default Notepad++ regex search + generate value to easily search and replace regex values.
Use the Notepad++'s built-in macro functionality for large files.

Acknowledgements
----------------
With thanks to:

* kbilsted for providing the excellent
[NotepadPlusPlusPluginPack.Net](https://github.com/kbilsted/NotepadPlusPlusPluginPack.Net)
* jokedst, [CsvQuery](https://github.com/jokedst/CsvQuery) was the inspiration for converting [datasetmultitool](https://github.com/BdR76/datasetmultitool) to a Notepad++ plug-in
* Members of the [Notepad++ plugin development community](https://community.notepad-plus-plus.org/category/5/plugin-development)
* Anyone who shared their [plug-in on GitHub](https://github.com/search?l=C%23&o=desc&q=notepad%2B%2B+plug&s=stars&type=Repositories)

The Random Values plug-in couldn't have been created without their source examples and suggestions.

Disclaimer
----------
This software is free-to-use and it is provided as-is without warranty of any kind,
always back-up your data files to prevent data loss.

History
-------
11-jul-2021 - first release

BdR©2021 Free to use - send questions or comments: Bas de Reuver - bdr1976@gmail.com

