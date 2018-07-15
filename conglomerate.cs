// Name:
// Submenu:
// Author:
// Title:
// Version:
// Desc:
// Keywords:
// URL:
// Help:

//DEFAULT TYPE
/***
void EFFECT(Surface dst, Surface src, Rectangle rect, ...){
    EFFECT effect = new EFFECT();
    PropertyCollection effectProps = effect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken effectParams = new PropertyBasedEffectConfigToken(effectProps);
    
    effectParams.SetPropertyValue(EFFECT.PropertyNames.PROPERTY, ...);
    //MORE PARAMETER SETS HERE
    
    effect.SetRenderInfo(effectParams, new RenderArgs(dst), new RenderArgs(src));
    // Call the Brightness and Contrast Adjustment function
    effect.Render(new Rectangle[1] {rect},0,1);
}
***/

public Rectangle layerClip;
public Surface mainLayer;


///BLENDING
private UserBlendOp normalOp = new UserBlendOps.NormalBlendOp(); // Normal
private UserBlendOp multiplyOp = new UserBlendOps.MultiplyBlendOp(); // Multiply
private UserBlendOp additiveOp = new UserBlendOps.AdditiveBlendOp(); // Additive
private UserBlendOp colorburnOp = new UserBlendOps.ColorBurnBlendOp(); // Color Burn
private UserBlendOp colordodgeOp = new UserBlendOps.ColorDodgeBlendOp(); // Color Dodge
private UserBlendOp reflectOp = new UserBlendOps.ReflectBlendOp(); // Reflect
private UserBlendOp glowOp = new UserBlendOps.GlowBlendOp(); // Glow
private UserBlendOp overlayOp = new UserBlendOps.OverlayBlendOp(); // Overlay
private UserBlendOp differenceOp = new UserBlendOps.DifferenceBlendOp(); // Difference
private UserBlendOp negationOp = new UserBlendOps.NegationBlendOp(); // Negation
private UserBlendOp lightenOp = new UserBlendOps.LightenBlendOp(); // Lighten
private UserBlendOp darkenOp = new UserBlendOps.DarkenBlendOp(); // Darken
private UserBlendOp screenOp = new UserBlendOps.ScreenBlendOp(); // Screen
private UserBlendOp xorOp = new UserBlendOps.XorBlendOp(); // Xor


///OPERATIONS
private UnaryPixelOps.Invert invertOp = new UnaryPixelOps.Invert();
private UnaryPixelOps.Desaturate desaturateOp = new UnaryPixelOps.Desaturate();
private UnaryPixelOps.HueSaturationLightness saturationOp;

void blackAndWhite(Surface dst, Surface src, Rectangle rect){
    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            CurrentPixel = src[x,y];
            CurrentPixel = desaturateOp.Apply(CurrentPixel);
            CurrentPixel = invertOp.Apply(CurrentPixel);
            dst[x,y] = CurrentPixel;
        }
    }
}

//I am not sure if it is a shallow copy, double assinment just in case
void invert(Surface dst, Surface src, Rectangle rect){
    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            CurrentPixel = src[x,y];
            CurrentPixel = invertOp.Apply(CurrentPixel);
            dst[x,y] = CurrentPixel;
        }
    }
}

//I am not sure if it is a shallow copy, double assinment just in case
void hueSat(Surface dst, Surface src, Rectangle rect,
    int hue, int sat, int light){
    saturationOp = new UnaryPixelOps.HueSaturationLightness(hue,sat,light);
    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            CurrentPixel = src[x,y];
            CurrentPixel = saturationOp.Apply(CurrentPixel);
            dst[x,y] = CurrentPixel;
        }
    }
}

void brightnessContrast(Surface dst, Surface src, Rectangle rect, 
    double bright, double contrast){
    BrightnessAndContrastAdjustment bacAdjustment = new BrightnessAndContrastAdjustment();
    PropertyCollection bacProps = bacAdjustment.CreatePropertyCollection();
    PropertyBasedEffectConfigToken bacParameters = new PropertyBasedEffectConfigToken(bacProps);
    bacParameters.SetPropertyValue(BrightnessAndContrastAdjustment.PropertyNames.Brightness, bright); // fix
    bacParameters.SetPropertyValue(BrightnessAndContrastAdjustment.PropertyNames.Contrast, contrast); // fix
    bacAdjustment.SetRenderInfo(bacParameters, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Brightness and Contrast Adjustment function
    bacAdjustment.Render(new Rectangle[1] {rect},0,1);
}

void softenPortait(Surface dst, Surface src, Rectangle rect, 
    double soft, double light, double warm){
    SoftenPortraitEffect effect = new SoftenPortraitEffect();
    PropertyCollection effectProps = effect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken effectParams = new PropertyBasedEffectConfigToken(effectProps);
    effectParams.SetPropertyValue(SoftenPortraitEffect.PropertyNames.Softness, soft);
    effectParams.SetPropertyValue(SoftenPortraitEffect.PropertyNames.Lighting, light);
    effectParams.SetPropertyValue(SoftenPortraitEffect.PropertyNames.Warmth, warm);
    effect.SetRenderInfo(effectParams, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Brightness and Contrast Adjustment function
    effect.Render(new Rectangle[1] {rect},0,1);
}

void dents(Surface dst, Surface src, Rectangle rect, 
    double scale, double refract, double rough, double tension, double quality){
    DentsEffect effect = new DentsEffect();
    PropertyCollection effectProps = effect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken effectParams = new PropertyBasedEffectConfigToken(effectProps);
    
    effectParams.SetPropertyValue(DentsEffect.PropertyNames.Scale, scale);
    effectParams.SetPropertyValue(DentsEffect.PropertyNames.Refraction, refract);
    effectParams.SetPropertyValue(DentsEffect.PropertyNames.Roughness, rough);
    effectParams.SetPropertyValue(DentsEffect.PropertyNames.Tension, tension);
    effectParams.SetPropertyValue(DentsEffect.PropertyNames.Quality, quality);
    
    
    effect.SetRenderInfo(effectParams, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Brightness and Contrast Adjustment function
    effect.Render(new Rectangle[1] {rect},0,1);
}

void glow(Surface dst, Surface src, Rectangle rect, 
    double radius, double bright, double contrast){
    GlowEffect effect = new GlowEffect();
    PropertyCollection effectProps = effect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken effectParams = new PropertyBasedEffectConfigToken(effectProps);
    
    effectParams.SetPropertyValue(GlowEffect.PropertyNames.Radius, radius);
    effectParams.SetPropertyValue(GlowEffect.PropertyNames.Brightness, bright);
    effectParams.SetPropertyValue(GlowEffect.PropertyNames.Contrast, contrast);
    
    effect.SetRenderInfo(effectParams, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Brightness and Contrast Adjustment function
    effect.Render(new Rectangle[1] {rect},0,1);
}

void motionBlur(Surface dst, Surface src, Rectangle rect, 
    double angle, Boolean centered, double distance){
    MotionBlurEffect blurEffect = new MotionBlurEffect();
    PropertyCollection blurProps = blurEffect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken BlurParameters = new PropertyBasedEffectConfigToken(blurProps);
    BlurParameters.SetPropertyValue(MotionBlurEffect.PropertyNames.Angle, angle); // fix
    BlurParameters.SetPropertyValue(MotionBlurEffect.PropertyNames.Centered, centered); // fix
    BlurParameters.SetPropertyValue(MotionBlurEffect.PropertyNames.Distance, distance); // fix
    blurEffect.SetRenderInfo(BlurParameters, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Motion Blur function
    blurEffect.Render(new Rectangle[1] {rect},0,1);
}

void median(Surface dst, Surface src, Rectangle rect, 
    double percent, double radius){
    MedianEffect effect = new MedianEffect();
    PropertyCollection effectProps = effect.CreatePropertyCollection();
    PropertyBasedEffectConfigToken effectParams = new PropertyBasedEffectConfigToken(effectProps);
    
    effectParams.SetPropertyValue(MedianEffect.PropertyNames.Percentile, percent);
    effectParams.SetPropertyValue(MedianEffect.PropertyNames.Radius, 3);
    
    effect.SetRenderInfo(effectParams, new RenderArgs(dst), new RenderArgs(dst.Clone()));
    // Call the Brightness and Contrast Adjustment function
    effect.Render(new Rectangle[1] {rect},0,1);
}


Surface copySurface(Surface src){
    Surface copied = new Surface(src.Width, src.Height);
    
    copied.CopySurface(src);
    
    return copied;
}

void blendSurfaceDown(Surface lower, Surface upper, UserBlendOp blend, 
    int lowerSurfaceAlpha, int upperSurfaceAlpha){
    ColorBgra CurrentPixel, lowerPixel;
    for (int y = 0; y < lower.Height; y++)
    {
        for (int x = 0; x < lower.Width; x++)
        {
            CurrentPixel = upper[x,y];
            lowerPixel = lower[x,y];
            //CurrentPixel.A = (byte)((CurrentPixel.A*upperSurfaceAlpha)/255);
            //lowerPixel.A = (byte)((lowerPixel.A*lowerSurfaceAlpha)/255);
            blend.Apply(lowerPixel, CurrentPixel);
            lower[x,y] = CurrentPixel;
        }
    }
}








///MAIN ENTRYPOINT
void Render(Surface dst, Surface src, Rectangle rect)
{
    Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();
    int CenterX = ((selection.Right - selection.Left) / 2) + selection.Left;
    int CenterY = ((selection.Bottom - selection.Top) / 2) + selection.Top;
	
	//things
	
}
