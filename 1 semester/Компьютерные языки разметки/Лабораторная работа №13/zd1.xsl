<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html>
    <body >
        <table align="center" border="1">
            <tr>
                <th>Песня</th>
                <th>Дата</th>
            </tr>
            <xsl:for-each select="list/read">
            <xsl:sort select="song"/>
                <tr>
                    <td><xsl:value-of select="song"/></td>
                    <td><xsl:value-of select="data"/></td>
                </tr>
            </xsl:for-each>
        </table>
    </body>
</html>
</xsl:template>
</xsl:stylesheet>