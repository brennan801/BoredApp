# github sudoers config
```
github ALL = (root) NOPASSWD: /usr/bin/systemctl daemon-reload, /usr/bin/systemctl start dotnetservice.service, /usr/bin/cp deployment/dotnetservice.service /lib/systemd/system/dotnetservice.service
```