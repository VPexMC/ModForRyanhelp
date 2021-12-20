using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModForRyan.Items;
using ModForRyan.Classes;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModForRyan
{
	// This file showcases the concept of piercing.
	// The code of the item that spawns it is located at the bottom.

	// NPC.immune determines if an npc can be hit by a item or projectile owned by a particular player (it is an array, each slot corresponds to different players (whoAmI))
	// NPC.immune is decremented towards 0 every update
	// Melee items set NPC.immune to player.itemAnimation, which starts at item.useAnimation and decrements towards 0
	// Projectiles, however, provide mechanisms for custom immunity.
	// 1. penetrate == 1: A projectile with penetrate set to 1 in SetDefaults will hit regardless of the npc's immunity counters (The penetrate from SetDefaults is remembered in maxPenetrate)
	//	Ex: Wooden Arrow.
	// 2. No code and penetrate > 1 or -1: npc.immune[owner] will be set to 10.
	// 	The NPC will be hit if not immune and will become immune to all damage for 10 ticks
	// 	Ex: Unholy Arrow
	// 3. Override OnHitNPC: If not immune, when it hits it manually set an immune other than 10
	// 	Ex: Arkhalis: Sets it to 5
	// 	Ex: Sharknado Minion: Sets to 20
	// 	Video: https://gfycat.com/DisloyalImprobableHoatzin Notice how Sharknado minion hits prevent Arhalis hits for a brief moment.
	// 4. Projectile.usesIDStaticNPCImmunity and Projectile.idStaticNPCHitCooldown: Specifies that a type of projectile has a shared immunity timer for each npc.
	// 	Use this if you want other projectiles a chance to damage, but don't want the same projectile type to hit an npc rapidly.
	// 	Ex: Ghastly Glaive is the only one who uses this.
	// 5. Projectile.usesLocalNPCImmunity and Projectile.localNPCHitCooldown: Specifies the projectile manages it's own immunity timers for each npc
	// 	Use this if you want the multiple projectiles of the same type to have a chance to attack rapidly, but don't want a single projectile to hit rapidly. A -1 value prevents the same projectile from ever hitting the npc again.
	// 	Ex: Lightning Aura sentries use this. (localNPCHitCooldown = 3, but other code controls how fast the projectile itself hits)
	// 		Overlapping Auras all have a chance to hit after each other even though they share the same ID.
	// Try the above by uncommenting out the respective bits of code in the projectile below.



	// VGPROJ1 stands for Video Game Projectile(1)
	public class VGPROJ1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Video Game Projectile"); // The name of the projectile(it can be appeared in chat)
		}

		public override void SetDefaults()
		{
			Projectile.width = 22; // The width of projectile hitbox
			Projectile.height = 16; // The height of projectile hitbox

			// Ccopy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			AIType = ProjectileID.Starfury;

			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.DamageType = ModContent.GetInstance<Arcade>(); // Is the projectile shoot by a ranged weapon?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 180; // Each update timeLeft is decreased by 1. Once timeLeft hits 0, the Projectile will naturally despawn. (60 ticks = 1 second)

			Projectile.penetrate = -1;
			// 1: Projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			// 2a: Projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			// 2b: Projectile.penetrate = 3; // Same, but max 3 hits before dying
			// 5: Projectile.usesLocalNPCImmunity = true;
			// 5a: Projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			// 5b: Projectile.localNPCHitCooldown = 20; // up to 20 hits
		}

		// See comments at the beginning of the class
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			// 3a: target.immune[Projectile.owner] = 20;
			// 3b: target.immune[Projectile.owner] = 5;
		}
	}
}