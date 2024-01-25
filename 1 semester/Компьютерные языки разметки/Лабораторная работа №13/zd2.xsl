<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html>
    <body >
        <table align="center" border="1">
            <tr >
                <th>Имя</th>
                <th>Фамилия</th>
                <th>Математика</th>
                <th>Английский</th>
            </tr>
            <xsl:for-each select="class/student">
            <xsl:sort select="name"/>
                <tr>
                    <td><xsl:value-of select="name"/></td>
                    <td><xsl:value-of select="sname"/></td>
                    <xsl:choose>
                            <xsl:when test="math &lt; 4">
                                <td bgcolor="red"><xsl:value-of select="math"/></td>
                            </xsl:when>
                            <xsl:when test="math &gt; 8">
                                <td bgcolor="green"><xsl:value-of select="math"/></td>
                            </xsl:when>
                            <xsl:otherwise>
                                <td><xsl:value-of select="math"/></td>
                            </xsl:otherwise>
                    </xsl:choose>
                    <xsl:choose>
                            <xsl:when test="english &lt; 4">
                                <td bgcolor="red"><xsl:value-of select="english"/></td>
                            </xsl:when>
                            <xsl:when test="english &gt; 8">
                                <td bgcolor="green"><xsl:value-of select="english"/></td>
                            </xsl:when>
                            <xsl:otherwise>
                                <td><xsl:value-of select="english"/></td>
                            </xsl:otherwise>
                    </xsl:choose>
                </tr>
            </xsl:for-each>
        </table>
    </body>
</html>
</xsl:template>
</xsl:stylesheet>