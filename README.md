# MinecraftServerAdmin
An ASP.NET Core MVC app that connects to a Minecraft server using RCON.

One notable feature of this app is that it relies on an external service to authenticate users, specifically an OAuth2/OIDC server.

The project is in an unfinished state. Currently implemented features include:

- View list of online players
- View and modify the whitelist
- (Un)ban players and IP addresses

Planned features include:

- Viewing the banlist (see below)
- Watch chat (not possible using RCON)
- Issue commands (need to ensure that permissions are honored)

### Minecraft's RCON bug
Minecraft's RCON implementation has a bug that causes all newlines to disappear from command output. There exists a [Forge mod](https://github.com/fraenkelc/RCONNewlineFix) for version prior to 1.17 that solves this problem. For version 1.17, Minecraft Forge fixes this problem directly. I'm working on a Fabric mod that fixes this problem.
