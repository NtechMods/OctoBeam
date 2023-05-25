using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef.SpawnType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.CustomScalesDef.SkipMode;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.PatternDef.PatternModes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef.TimedSpawnDef.PointTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.Conditions;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.UpRelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.FwdRelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.ReInitCondition;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.RelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.ConditionOperators;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.StageEvents;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DeformDef.DeformTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.Falloff;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.AoeShape;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.EwarMode;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.EwarType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.PushPullDef.Force;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.FactionColor;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.DecalDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Phasers_Ammo_Red_Beam_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Red BL", 
            HybridRound = false, 
            EnergyCost = 1000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Red Beam Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 3f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.5f, //
                        Color = Color(red: 30, green: 15, blue: 15, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Red_1",
                            "Beam_Red_2",
                            "Beam_Red_3",
                            "Beam_Red_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Beam_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Red Beam Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 1f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Beam_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Orange BL", 
            HybridRound = false, 
            EnergyCost = 2000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.33f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Orange Beam Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 6f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.5f, //
                        Color = Color(red: 30, green: 15, blue: 0, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Orange_1",
                            "Beam_Orange_2",
                            "Beam_Orange_3",
                            "Beam_Orange_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Beam_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Orange Beam Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 2f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Beam_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Green BL", 
            HybridRound = false, 
            EnergyCost = 3000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.67f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Green Beam Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 9f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.5f, //
                        Color = Color(red: 15, green: 30, blue: 15, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Green_1",
                            "Beam_Green_2",
                            "Beam_Green_3",
                            "Beam_Green_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Beam_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Green Beam Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 3f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Beam_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Blue BL", 
            HybridRound = false, 
            EnergyCost = 4000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 2f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Blue Beam Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 12f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.5f, //
                        Color = Color(red: 15, green: 15, blue: 30, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Blue_1",
                            "Beam_Blue_2",
                            "Beam_Blue_3",
                            "Beam_Blue_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Beam_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Blue Beam Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 4f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Pulse_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Red PL", 
            HybridRound = false, 
            EnergyCost = 1000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Red Pulse Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 3f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Red", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 50f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 30, green: 15, blue: 15, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 25f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Pulse_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Red Pulse Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 1f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Pulse_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Orange PL", 
            HybridRound = false, 
            EnergyCost = 2000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.33f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Orange Pulse Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 6f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Orange", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 50f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 15, green: 7.5f, blue: 0, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 25f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Pulse_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Orange Pulse Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 2f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Pulse_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Green PL", 
            HybridRound = false, 
            EnergyCost = 3000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.67f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Green Pulse Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 9f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Green", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 50f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 15, green: 30, blue: 15, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 10f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Pulse_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Green Pulse Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 3f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Pulse_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Blue PL", 
            HybridRound = false, 
            EnergyCost = 4000f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 2f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Blue Pulse Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 12f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 5000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Blue", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 50f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 15, green: 15, blue: 30, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 15f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Pulse_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Blue Pulse Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10f,
                    Damage = 4f,
                    Depth = 10f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Beam_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Red BS", 
            HybridRound = false, 
            EnergyCost = 200f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Red Beam Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.6f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.2f, //
                        Color = Color(red: 30, green: 15, blue: 15, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Red_1",
                            "Beam_Red_2",
                            "Beam_Red_3",
                            "Beam_Red_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Beam_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Red Beam Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.2f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Beam_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Orange BS", 
            HybridRound = false, 
            EnergyCost = 400f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.33f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Orange Beam Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 1.2f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.2f, //
                        Color = Color(red: 30, green: 15, blue: 0, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Orange_1",
                            "Beam_Orange_2",
                            "Beam_Orange_3",
                            "Beam_Orange_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Beam_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Orange Beam Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.4f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Beam_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Green BS", 
            HybridRound = false, 
            EnergyCost = 600f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.67f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Green Beam Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 1.8f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.2f, //
                        Color = Color(red: 15, green: 30, blue: 15, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Green_1",
                            "Beam_Green_2",
                            "Beam_Green_3",
                            "Beam_Green_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Beam_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Green Beam Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.6f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Beam_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Blue BS", 
            HybridRound = false, 
            EnergyCost = 800f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 2f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Blue Beam Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 2.4f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                MaxTrajectoryTime = 60,
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.2f, //
                        Color = Color(red: 15, green: 15, blue: 30, alpha: 1), 
                        VisualFadeStart = 80, 
                        VisualFadeEnd = 90, 
                        Textures = new[] {
                            "Beam_Blue_1",
                            "Beam_Blue_2",
                            "Beam_Blue_3",
                            "Beam_Blue_4", 
                        },
                        TextureMode = Cycle, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Beam_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Blue Beam Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.8f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Pulse_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Red PS", 
            HybridRound = false, 
            EnergyCost = 200f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Red Pulse Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.6f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Red", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 20f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 30, green: 15, blue: 15, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 5f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Red_Pulse_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Red Pulse Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.2f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Pulse_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Orange PS", 
            HybridRound = false, 
            EnergyCost = 400f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.33f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Orange Pulse Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 1.2f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Orange", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 20f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 30, green: 15, blue: 0, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 5f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Orange_Pulse_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Orange Pulse Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.4f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Pulse_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Green PS", 
            HybridRound = false, 
            EnergyCost = 600f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 1.67f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Green Pulse Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 1.8f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Green", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 20f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 22.5f, green: 45, blue: 22.5f, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 2f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Green_Pulse_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Green Pulse Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.6f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Pulse_Small_Primary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Blue PS", 
            HybridRound = false, 
            EnergyCost = 800f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = true, 
            EnergyMagazineSize = 3600, 
            HeatModifier = 2f,

            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "Phasers Ammo Blue Pulse Small Secondary", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                ArmWhenHit = false, // Setting this to true will arm the projectile when its shot by other projectiles.
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below, unless ArmWhenHit or Eol ArmOnlyOnHit is set to true then both kinds of frags are active
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 100f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 2.4f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 0, 
                MaxTrajectory = 2000f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 256f, //
                        Width = 0.25f, //
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        FactionColor = DontUse, // DontUse, Foreground, Background.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "Transparent_Beam", // Please always have this Line set, if this Section is enabled.
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = true, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "Pulse_Blue", // Please always have this Line set, if this Section is enabled.
                            },
                            SegmentLength = 20f, // Uses the values below.
                            SegmentGap = 1500f, // Uses Tracer textures and values
                            Speed = 5000f, // meters per second
                            Color = Color(red: 22.5f, green: 22.5f, blue: 45, alpha: 1),
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            WidthMultiplier = 3f,
                            Reverse = false, 
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };

        private AmmoDef Phasers_Ammo_Blue_Pulse_Small_Secondary => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", 
            AmmoRound = "Phasers Ammo Blue Pulse Small Secondary", 
            HybridRound = false, 
            EnergyCost = 0.001f, 
            BaseDamage = 1f, 
            Health = 0,
            HardPointUsable = false, 
            EnergyMagazineSize = 1, 

            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = -1f, // Distance at which damage begins falling off.
                    MinMultipler = -1f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = -1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    Types = new[] // List of blocks to apply custom damage multipliers to.
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 2f,
                    Damage = 0.8f,
                    Depth = 2f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff,
                    Shape = Diamond,
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1f, 
                    Damage = 0.001f,
                    Depth = 1f,
                    MaxAbsorb = 0f,
                    Falloff = Pooled, 
                    MinArmingTime = 0, 
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "",
                    CustomSound = "", 
                }, 
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, 
                TargetLossDegree = 0f, 
                MaxLifeTime = 0, 
                AccelPerSec = 0f, 
                DesiredSpeed = 1000, 
                MaxTrajectory = 250f, 
                TotalAcceleration = 0,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", 
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "ST_Explosion",
                        ApplyToShield = true,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = true,
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 0.005f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 0f),
                    WidthVariance = Random(start: 0f, end: 0f), 
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f, //
                        Width = 0.1f, //
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), 
                        VisualFadeStart = 0, 
                        VisualFadeEnd = 0, 
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal, 
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", 
                        },
                        TextureMode = Normal,
                        DecayTime = 3, 
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                HitSound = "ArcWepLargeShellEpl",
                HitPlayChance = 0.005f,
                HitPlayShield = false,
            },
        };
    }
}