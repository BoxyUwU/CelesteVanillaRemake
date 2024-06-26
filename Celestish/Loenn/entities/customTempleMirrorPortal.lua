local drawableSprite = require("structs.drawable_sprite")

local custom_temple_mirror_portal = {}

custom_temple_mirror_portal.name = "Celestish/CustomTempleMirrorPortal"
custom_temple_mirror_portal.depth = -1999
custom_temple_mirror_portal.placements = {
    name = "Custom Temple Mirror Portal",
    data = {
        level = "",
        to_seeker = false,
        aside_intro = false,
        aside_music = false,
    }
}

local frameTexture = "objects/temple/portal/portalframe"
local curtainTexture = "objects/temple/portal/portalcurtain00"
local torchTexture = "objects/temple/portal/portaltorch00"

local torchOffset = 90


function custom_temple_mirror_portal.sprite(room, entity)
    local frameSprite = drawableSprite.fromTexture(frameTexture, entity)
    local curtainSprite = drawableSprite.fromTexture(curtainTexture, entity)
    local torchSpriteLeft = drawableSprite.fromTexture(torchTexture, entity)
    local torchSpriteRight = drawableSprite.fromTexture(torchTexture, entity)

    torchSpriteLeft:addPosition(-torchOffset, 0)
    torchSpriteLeft:setJustification(0.5, 0.75)

    torchSpriteRight:addPosition(torchOffset, 0)
    torchSpriteRight:setJustification(0.5, 0.75)

    local sprites = {
        frameSprite,
        curtainSprite,
        torchSpriteLeft,
        torchSpriteRight
    }

    return sprites
end

return custom_temple_mirror_portal
