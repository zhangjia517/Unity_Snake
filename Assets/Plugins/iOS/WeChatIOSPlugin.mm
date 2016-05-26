//
//  WeChatIOSPlugin.m
//  WeChatIOSPlugin
//
//  Created by macmini on 11/6/15.
//  Copyright © 2015 macmini. All rights reserved.
//

#import "WeChatIOSPlugin.h"

@implementation WeChatIOSPlugin

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url {
    return [WXApi handleOpenURL:url delegate:self];
}
- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation {
    return [WXApi handleOpenURL:url delegate:self];
}

- (void) onReq:(BaseReq*)req
{
    
}
- (void) onResp:(BaseResp*)resp
{
    if([resp isKindOfClass:[SendMessageToWXResp class]]) {
        NSString *strMsg = [NSString stringWithFormat:@"Result:%d", resp.errCode];
        NSLog(@"Response from WeChat was: %@",strMsg);
    }
}

@end

UIImage* thumbimagechosen;
bool ismoment;

extern "C" void SetIsMoments(bool value)
{
    ismoment = value;
}

extern "C" void SetThumbImageWithUrl(const char* url)
{
    NSURLRequest *URLreq = [NSURLRequest requestWithURL:[NSURL URLWithString: [NSString stringWithUTF8String:url]]];
    bool valid = [NSURLConnection canHandleRequest:URLreq];
    UIImage *image;
    
    if(valid)
    {
        NSURL *imageURL = [NSURL URLWithString: [[NSString stringWithUTF8String:url] stringByAddingPercentEscapesUsingEncoding:NSUTF8StringEncoding]];
        NSData *imageData = [NSData dataWithContentsOfURL:imageURL];
        image = [UIImage imageWithData:imageData];
    }
    
    else
        NSLog(@"Invalid Url");
    
    thumbimagechosen = image;
}

extern "C" void SetThumbImageWithPath(const char* url)
{
    NSURLRequest *URLreq = [NSURLRequest requestWithURL:[NSURL URLWithString: [NSString stringWithUTF8String:url]]];
    bool valid = [NSURLConnection canHandleRequest:URLreq];
    UIImage *image;
    
    if(valid)
        image = [UIImage imageWithContentsOfFile:[NSString stringWithUTF8String:url]];
    
    else
        NSLog(@"Invalid Url");
    
    thumbimagechosen = image;
}

extern "C" void SetThumbImageWithTexture()
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory,
                                                         NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    NSString* path = [documentsDirectory stringByAppendingPathComponent:
                      @"/wechatshare.png" ];
    UIImage* image = [UIImage imageWithContentsOfFile:path];
    thumbimagechosen = image;
}

extern "C" void ShareImageWithTexture(const char* title, const char* desc)
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory,
                                                         NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    NSString* path = [documentsDirectory stringByAppendingPathComponent:
                      @"/wechatshare.png" ];
    UIImage* actualimage = [UIImage imageWithContentsOfFile:path];
    NSData *imageData = UIImagePNGRepresentation(actualimage);
    
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String:title];
    message.description = [NSString stringWithUTF8String:desc];
    [message setThumbImage: thumbimagechosen];
    WXImageObject *ext = [WXImageObject object];
    ext.imageData = imageData;
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareImageWithPath(const char* path, const char* title, const char* desc)
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String:title];
    message.description = [NSString stringWithUTF8String:desc];
    [message setThumbImage: thumbimagechosen];
    WXImageObject *ext = [WXImageObject object];
    NSString *filePath = [[NSBundle mainBundle] pathForResource:@"res1" ofType:@"jpg"];
    ext.imageData = [NSData dataWithContentsOfFile:filePath];
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareImageWithURL(const char* path, const char* title, const char* desc)
{
    NSURL *imageURL = [NSURL URLWithString: [[NSString stringWithUTF8String:path] stringByAddingPercentEscapesUsingEncoding:NSUTF8StringEncoding]];
    NSData *imageData = [NSData dataWithContentsOfURL:imageURL];
    
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String:title];
    message.description = [NSString stringWithUTF8String:desc];
    [message setThumbImage: thumbimagechosen];
    WXImageObject *ext = [WXImageObject object];
    ext.imageData = imageData;
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareText(const char* text, const char* desc)
{
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.text = [NSString stringWithUTF8String:text];
    req.bText = YES;
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    [WXApi sendReq:req];
}

extern "C" void ShareLink (const char* url , const char * title, const char* description )
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String: title];
    message.description = [NSString stringWithUTF8String: description];
    WXWebpageObject *ext = [WXWebpageObject object];
    message.mediaObject = ext;
    [message setThumbImage: thumbimagechosen];
    ext.webpageUrl = [NSString stringWithUTF8String:url];
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareMusicWithLink(const char* url, const char* title, const char* desc)
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String: title];
    message.description = [NSString stringWithUTF8String: desc];
    [message setThumbImage: thumbimagechosen];
    WXMusicObject *ext = [WXMusicObject object];
    ext.musicUrl = [NSString stringWithUTF8String:url];
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareMusicWithLowBandUrl(const char* url, const char* title, const char* desc)
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String: title];
    message.description = [NSString stringWithUTF8String: desc];
    [message setThumbImage: thumbimagechosen];
    WXMusicObject *ext = [WXMusicObject object];
    ext.musicLowBandUrl = [NSString stringWithUTF8String:url];
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareVideoWithLink(const char* url, const char* title, const char* desc)
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String:title];
    message.description = [NSString stringWithUTF8String:desc];
    [message setThumbImage: thumbimagechosen];
    
    WXVideoObject *ext = [WXVideoObject object];
    ext.videoUrl = [NSString stringWithUTF8String:url];
    
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" void ShareVideoWithLowBandURL(const char* url, const char* title, const char* desc)
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = [NSString stringWithUTF8String: title];
    message.description = [NSString stringWithUTF8String: desc];
    [message setThumbImage: thumbimagechosen];
    WXVideoObject *ext = [WXVideoObject object];
    ext.videoLowBandUrl = [NSString stringWithUTF8String:url];
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    
    if(ismoment)
        req.scene = WXSceneTimeline;
    else
        req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

extern "C" bool isWeChatInstalled()
{
    bool installed;
    if (![WXApi isWXAppInstalled])
        installed = false;
    else
        installed = true;
    
    return installed;
}

extern "C" void InitWeChat(const char* app_id)
{
    if ([WXApi registerApp: [NSString stringWithUTF8String:app_id]])
        NSLog(@"Successful to register with WeChat");
    else
        NSLog(@"Failed to register with WeChat");
    
}
