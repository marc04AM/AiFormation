import socket, json, base64

code = r'''
import bpy
import tempfile, os, base64

# Switch to VIEW_3D area if not already
for screen in bpy.data.screens:
    for area in screen.areas:
        if area.type == 'VIEW_3D':
            # Override context
            with bpy.context.temp_override(window=bpy.context.window, area=area, region=area.regions[0]):
                bpy.ops.view3d.window_to_image()
'''

# Try a simpler approach: save screenshot to file
code2 = r'''
import bpy
import tempfile, os, base64

filepath = os.path.join(tempfile.gettempdir(), 'blender_scene_screenshot.png')
bpy.context.scene.render.filepath = filepath
bpy.ops.render.opengl(write_still=True, view_context=True)

with open(filepath, 'rb') as f:
    data = f.read()

result = {'screenshot_path': filepath, 'size': len(data), 'encoded': base64.b64encode(data).decode('utf-8')}
'''

request = json.dumps({'type': 'execute', 'code': code2, 'strict_json': True}) + '\0'
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.settimeout(300)
sock.connect(('localhost', 9876))
sock.sendall(request.encode('utf-8'))
buf = bytearray()
while True:
    chunk = sock.recv(65536)
    if not chunk:
        break
    buf.extend(chunk)
    if b'\0' in buf:
        break
sock.close()
line, _, _ = buf.partition(b'\0')
resp = json.loads(line.decode('utf-8'))

if resp.get('status') == 'ok':
    r = resp['result']
    # Write the image to a file
    img_data = base64.b64decode(r['encoded'])
    outpath = r'D:\DEV\AiFormation\screenshot.png'
    with open(outpath, 'wb') as f:
        f.write(img_data)
    print(f'Screenshot saved to {outpath}, size: {len(img_data)} bytes')
elif resp.get('status') == 'error':
    print('ERROR:', resp.get('message', 'unknown'))
else:
    print('UNKNOWN:', json.dumps(resp, indent=2, default=str)[:3000])
