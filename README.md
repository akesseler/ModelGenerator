<p align="center">
  <a href="https://github.com/akesseler/ModelGenerator/blob/master/LICENSE.md" alt="license">
    <img src="https://img.shields.io/github/license/akesseler/ModelGenerator.svg" />
  </a>
  <a href="https://github.com/akesseler/ModelGenerator/releases/latest" alt="latest">
    <img src="https://img.shields.io/github/release/akesseler/ModelGenerator.svg" />
  </a>
  <a href="https://github.com/akesseler/ModelGenerator/archive/master.zip" alt="master">
    <img src="https://img.shields.io/github/languages/code-size/akesseler/ModelGenerator.svg" />
  </a>
</p>

## Model Generator

The _Plexdata Model Generator_ is a program that is able to generate source code files for C# 
and Visual Basic which are based on pure JSON and/or XML files.

### Licensing

The software has been published under the terms of _MIT License_.

### Downloads

The latest release can be obtained from [https://github.com/akesseler/modelgenerator/releases/latest](https://github.com/akesseler/modelgenerator/releases/latest).

The master branch can be downloaded as ZIP from [https://github.com/akesseler/modelgenerator/archive/master.zip](https://github.com/akesseler/modelgenerator/archive/master.zip).

## Known Bugs

Under some circumstances it is possible that a generated class contains an item of some type 
as well as a list of that type. In such a case it will be necessary to remove the redundant 
item manually. Here an example.

_Source XML_

```
<LabelCollection>
  <Label>Business</Label>
</LabelCollection>

...

<LabelCollection>
  <Label>Fax</Label>
  <Label>Business</Label>
</LabelCollection>
```

_Generated Class_

```
[XmlRoot("LabelCollection")]
public class LabelCollection
{
  [XmlElement("Label")]
  public Label Label { get; set; } // Must be removed manually.

  [XmlElement("Label")]
  public List<Label> Labels { get; set; }
}
```

The reason behind, the generator is unable at the moment to distinguish between item types and 
a list of items of the same type.
