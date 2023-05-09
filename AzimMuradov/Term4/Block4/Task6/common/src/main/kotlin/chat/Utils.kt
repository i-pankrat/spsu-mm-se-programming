package chat

import mu.KotlinLogging
import java.io.InputStreamReader
import java.io.PrintWriter
import java.net.Socket


val logger = KotlinLogging.logger("IO")

fun <T> tryOrNull(b: () -> T): T? = try {
    b()
} catch (e: Exception) {
    null
}

fun Socket.reader(): InputStreamReader = getInputStream().reader()

fun Socket.printer(): PrintWriter = PrintWriter(getOutputStream().writer(), true)
