using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shotå^ñÇñ@Çê∂ê¨Ç∑ÇÈFactoryÉNÉâÉX
public class ShotMagicFactory
{
    public void Create(ShotMagicConfig config, Vector3 position, int strength, int direction, WizardAgent agent)
    {
        var instance = Object.Instantiate(config.Prefab, position, Quaternion.identity);

        var initContext = new ShotMagicInitContext(
            config.Power, 
            strength,
            config.Speed, 
            direction, 
            config.Duration,
            agent
        );

        instance.GetComponent<ShotMagic>().Initialize(initContext);
    }
}
