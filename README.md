SCP-507

Config:
```
scp507:
  is_enabled: true
  debug: false
  # Scp507 Teleporter: Minimum time (seconds)
  scp507_teleporter_min: 2
  # Scp507 Teleporter: Maximum time (seconds)
  scp507_teleporter_max: 135
  # Custom Roles: Settings
  scp507:
    id: 507
    role: Scientist
    max_health: 350
    name: 'SCP-507'
    description: ''
    custom_info: 'SCP-507'
    custom_abilities: []
    inventory: []
    ammo: {}
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points: []
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    scale:
      x: 1
      y: 1
      z: 1
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
```
