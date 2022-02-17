# github sudoers config
```
github ALL = (root) NOPASSWD: /usr/bin/systemctl daemon-reload, /usr/bin/systemctl restart dotnetservice.service, /usr/bin/systemctl restart wg-quick@wg0
```