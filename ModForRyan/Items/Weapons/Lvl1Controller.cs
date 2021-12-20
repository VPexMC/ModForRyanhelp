using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ModForRyan.Classes;
using ModForRyan;

namespace ModForRyan.Items.Weapons
{
	public class Lvl1Controller : ModItem
	{
		public override void SetStaticDefaults()
		{
            Tooltip.SetDefault("Shoots Games");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 34;

			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.autoReuse = false;

			Item.DamageType = ModContent.GetInstance<Arcade>();
			Item.damage = 7;
			Item.knockBack = 3;
			Item.crit = 3;

			Item.value = Item.buyPrice(silver: 15);
			Item.rare = ItemRarityID.Green;
			// Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/EightBit");

            Item.shoot = ModContent.ProjectileType<VGPROJ1>();
			Item.shootSpeed = 16f; // Speed of the projectiles the sword will shoot
		}
		// This method gets called when firing your weapon/sword.
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			// Loop these functions 3 times.
			for (int i = 0; i < 3; i++)
			{
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				position.Y -= 100 * i;
				Vector2 heading = target - position;

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FallenStar, 5)
				.AddRecipeGroup(ItemID.IronBar, 15)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}