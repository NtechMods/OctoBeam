using static WeaponThread.Session.ShieldDefinition.ShieldType;
using static WeaponThread.Session.AmmoTrajectory.GuidanceType;
using static WeaponThread.Session.HardPointDefinition.Prediction;
using static WeaponThread.Session.AreaDamage.AreaEffectType;
using static WeaponThread.Session.TargetingDefinition.BlockTypes;
using static WeaponThread.Session.TargetingDefinition.Threat;
using static WeaponThread.Session.Shrapnel.ShrapnelShape;
using static WeaponThread.Session.ShapeDefinition.Shapes;
using static WeaponThread.Session;

namespace WeaponThread
{   // Don't edit above this line
    partial class Weapons { WeaponDefinition OctoBeam => new WeaponDefinition {
    Assignments = new ModelAssignments
    {
        MountPoints = new[]
        {
            MountPoint(subTypeId: "OctoBeamLB", aimPartId: "GazerBeamLenses", muzzlePartId: "GazerBeamLenses"),
			MountPoint(subTypeId: "OctoBeamSB", aimPartId: "GazerBeamLenses", muzzlePartId: "GazerBeamLenses"),
        },
        Barrels = Names("muzzle_barrel_001", "muzzle_barrel_002", "muzzle_barrel_003", "muzzle_barrel_004", "muzzle_barrel_005", "muzzle_barrel_006", "muzzle_barrel_007", "muzzle_barrel_008"),
        EnableSubPartPhysics = false
    },
    HardPoint = new HardPointDefinition
    {
        WeaponId = "OctoBeam", // name of weapon in terminal
        AmmoMagazineId = "Blank",
        Block = AimControl(trackTargets: true, turretAttached: true, turretController: true, primaryTracking: true, rotateRate: 0.02f, elevateRate: 0.02f, minAzimuth: -180, maxAzimuth: 180, minElevation: -20, maxElevation: 80, offset: Vector(x: 0, y: 0, z: 0), fixedOffset: false, debug: false),
        DeviateShotAngle = 0f,
        AimingTolerance = 4f, // 0 - 180 firing angle
        EnergyCost = 0.02f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
        RotateBarrelAxis = 0, // 0 = off, 1 = xAxis, 2 = yAxis, 3 = zAxis
        AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
        DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
        GridWeaponCap = 12,// 0 = unlimited, the smallest weapon cap assigned to a subTypeId takes priority.
        Ui = Display(rateOfFire: true, damageModifier: true, toggleGuidance: false, enableOverload: true),

        Loading = new AmmoLoading
        {
            RateOfFire = 2048,
            BarrelsPerShot = 8,
            TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
            SkipBarrels = 0,
            ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
            DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
            HeatPerShot = 2, //heat generated per shot
            MaxHeat = 24000, //max heat before weapon enters cooldown (70% of max heat)
            Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
            HeatSinkRate = 600, //amount of heat lost per second
            DegradeRof = true, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
            ShotsInBurst = 512,
            DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
        },
    },
    Targeting = new TargetingDefinition
    {
        Threats = Valid(Characters, Projectiles, Grids),
        SubSystems = Priority(Offense, Thrust, Utility, Power, Production, Any), //define block type targeting order
        ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
        MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
        MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
        TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
        TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
        StopTrackingSpeed = 70, // do not track target threats traveling faster than this speed
    },
    DamageScales = new DamageScaleDefinition
    {
        MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
        DamageVoxels = false, // true = voxels are vulnerable to this weapon
        SelfDamage = false, // true = allow self damage.

        // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
        Characters = -1f,
        Grids = Options(largeGridModifier: -1f, smallGridModifier: -1f),
        Armor = Options(armor: -1f, light: -1f, heavy: -1f, nonArmor: -1f), 
        Shields = Options(modifier: -1f, type: Energy), // Types: Heal, Kinetic, Energy, Emp or Bypass

        // ignoreOthers will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
        Custom = SubTypeIds(false),
    },
    Ammo = new AmmoDefinition
    {
        BaseDamage = 6f,
        Mass = 0.1f, // in kilograms
        Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
        BackKickForce = 0f,
        Shape = Options(shape: Line, diameter: 0), //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
        ObjectsHit = Options(maxObjectsHit: 2, countBlocks: false), // 0 = disabled, value determines max objects (and/or blocks) penetrated per hit
        Shrapnel = Options(baseDamage: 1, fragments: 0, maxTrajectory: 100, noAudioVisual: true, noGuidance: true, shape: HalfMoon),

        AreaEffect = new AreaDamage
        {
            AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive is keens, Radiant is not.
            AreaEffectDamage = 0f, // 0 = use spillover from BaseDamage, otherwise use this value.
            AreaEffectRadius = 100f,
            Explosions = Options(noVisuals: false, noSound: false, scale: 1, customParticle: "", customSound: ""),
            Detonation = Options(detonateOnEnd: false, armOnlyOnHit: false, detonationDamage: 0, detonationRadius: 0),
            EwarFields = Options(duration: 600, stackDuration: true, depletable: true)
        },
        Beams = new BeamDefinition
        {
            Enable = true,
            VirtualBeams = true, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
            ConvergeBeams = true, // When using virtual beams this option visually converges the beams to the location of the real beam.
            RotateRealBeam = true, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
            OneParticle = true, // Only spawn one particle hit per beam weapon.
        },
        Trajectory = new AmmoTrajectory
        {
            Guidance = None,
            TargetLossDegree = 80f,
            TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
            AccelPerSec = 0f,
            DesiredSpeed = 0f,
            MaxTrajectory = 3000f,
            FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
            SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
            RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
            Smarts = new Smarts
            {
                Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                Aggressiveness = 1f, // controls how responsive tracking is.
                MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                TrackingDelay = 1, // Measured in line length units traveled.
                MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoints.
            },
            Mines = Options(detectRadius: 200, deCloakRadius: 100, fieldTime: 1800, cloak: true, persist: false),
        },
    },
    Graphics = new GraphicDefinition
    {
        ModelName = "",
        VisualProbability = 1f,
        ShieldHitDraw = true,
        Particles = new ParticleDefinition
        {
            Ammo = new Particle
            {
                Name = "ShipWelderArc",
                Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                Offset = Vector(x: 0, y: -1, z: 0),
                Extras = Options(loop: true, restart: false, distance: 5000, duration: 1, scale: 1)
            },
            Hit = new Particle
            {
                Name = "EnergyBlast",
                Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                Offset = Vector(x: 0, y: 0, z: 0),
                Extras = Options(loop: false, restart: false, distance: 5000, duration: 1, scale: 0.6f),
            },
            Barrel1 = new Particle
            {
                Name = "", // Smoke_LargeGunShot
                Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                Offset = Vector(x: 0, y: -1, z: 0),
                Extras = Options(loop: true, restart: false, distance: 200, duration: 1, scale: 1f),
            },
            Barrel2 = new Particle
            {
                Name = "EnergyBlast",//Muzzle_Flash_Large
                Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                Offset = Vector(x: 0, y: -1, z: 0),
                Extras = Options(loop: true, restart: false, distance: 200, duration: 1, scale: 1f),
			},
		},
        Line = new LineDefinition
        {
            Tracer = Base(enable: true, length: 1f, width: 0.05f, color: Color(red: 2, green: 2, blue: 30, alpha: 1)),
            TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
            ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
            WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
            Trail = Options(enable: false, material: "WeaponLaser", decayTime: 600, color: Color(red: 8, green: 8, blue: 64, alpha: 8)),
            OffsetEffect = Options(maxOffset: 0, minLength: 5, maxLength: 15), // 0 offset value disables this effect
        },
    },
    Audio = new AudioDefinition
    {
        HardPoint = new AudioHardPointDefinition
        {
            FiringSound = "BeamHum", // WepShipGatlingShot
            FiringSoundPerShot = false,
            ReloadSound = "",
            NoAmmoSound = "",
            HardPointRotationSound = "WepTurretGatlingRotate",
            BarrelRotationSound = "",
        },

        Ammo = new AudioAmmoDefinition
        {
            TravelSound = "",
            HitSound = "",
        }, // Don't edit below this line
    },
};}}





