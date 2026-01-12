using System.Collections.Generic;
using _2D_MonoGame_Engine.World;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace _2D_MonoGame_Engine.Sound;

public class SoundManager
{
    private Dictionary<string, SoundEffect> _sounds;
    
    //TODO:: Object pool!
    private Dictionary<string, SoundEffectInstance> _soundInstances;



    public SoundManager()
    {
        _sounds = new Dictionary<string, SoundEffect>();
        _soundInstances = new Dictionary<string, SoundEffectInstance>();
    }

//TODO:: Merge these into 1 system
    public void LoadSoundEffect(string name)
    {
        ContentManager manager = Globals.ContentManager;
        
        if (name != null)
        {
            //Get the sound effect
            var sound = manager.Load<SoundEffect>(name);
            //Add to dictionary
            _sounds.Add(name, sound);
        }
    }


    public void PlaySoundEffect(string name)
    {
        if(_sounds.ContainsKey(name))
            _sounds[name].Play();
    }
    
    public void CreateSoundEffectInstance(string name)
    {
        if(_sounds.ContainsKey(name))
            _soundInstances.Add(name, _sounds[name].CreateInstance());
    }
    
    public void PlaySoundEffectInstance(string name)
    {
        if(_soundInstances.ContainsKey(name))
            _soundInstances[name].Play();
    } 
    
    //-------
    
}